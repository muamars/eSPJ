const CACHE_NAME = "espj-v1.0.0";
const urlsToCache = [
  "/",
  "/login",
  "/driver/manifest.json",
  "/driver/icon-192.png",
  "/driver/icon-512.png",
  "https://unpkg.com/lucide@latest",
  "https://fonts.googleapis.com/css2?family=Inter+Tight:ital,wght@0,100..900;1,100..900&display=swap",
];

// Install Service Worker
self.addEventListener("install", (event) => {
  console.log("[SW] Installing...");
  event.waitUntil(
    caches
      .open(CACHE_NAME)
      .then((cache) => {
        console.log("[SW] Caching app shell");
        return cache.addAll(urlsToCache);
      })
      .then(() => {
        console.log("[SW] Skip waiting");
        return self.skipWaiting();
      })
  );
});

// Activate Service Worker
self.addEventListener("activate", (event) => {
  console.log("[SW] Activating...");
  event.waitUntil(
    caches
      .keys()
      .then((cacheNames) => {
        return Promise.all(
          cacheNames.map((cacheName) => {
            if (cacheName !== CACHE_NAME) {
              console.log("[SW] Deleting old cache:", cacheName);
              return caches.delete(cacheName);
            }
          })
        );
      })
      .then(() => {
        console.log("[SW] Claiming clients");
        return self.clients.claim();
      })
  );
});

// Fetch Event - Cache Strategy: Network First with Cache Fallback
self.addEventListener("fetch", (event) => {
  // Skip cross-origin requests
  if (!event.request.url.startsWith(self.location.origin)) {
    return;
  }

  event.respondWith(
    fetch(event.request)
      .then((response) => {
        // Check if we received a valid response
        if (!response || response.status !== 200 || response.type !== "basic") {
          return response;
        }

        // Clone the response
        const responseToCache = response.clone();

        caches.open(CACHE_NAME).then((cache) => {
          cache.put(event.request, responseToCache);
        });

        return response;
      })
      .catch(() => {
        // Network failed, serve from cache
        return caches.match(event.request).then((response) => {
          if (response) {
            return response;
          }

          // If no cache match, return offline page for navigation requests
          if (event.request.mode === "navigate") {
            return caches.match("/offline.html");
          }
        });
      })
  );
});

// Background Sync for offline actions
self.addEventListener("sync", (event) => {
  console.log("[SW] Background sync:", event.tag);

  if (event.tag === "background-sync-login") {
    event.waitUntil(handleBackgroundLogin());
  }
});

// Push Notifications
self.addEventListener("push", (event) => {
  console.log("[SW] Push received:", event.data?.text());

  const options = {
    body: event.data?.text() || "Notifikasi dari eSPJ",
    icon: "/icon-192.png",
    badge: "/icon-72.png",
    vibrate: [100, 50, 100],
    data: {
      dateOfArrival: Date.now(),
      primaryKey: "1",
    },
    actions: [
      {
        action: "explore",
        title: "Buka Aplikasi",
        icon: "/icon-96.png",
      },
      {
        action: "close",
        title: "Tutup",
        icon: "/icon-96.png",
      },
    ],
  };

  event.waitUntil(self.registration.showNotification("eSPJ", options));
});

// Notification Click Handler
self.addEventListener("notificationclick", (event) => {
  console.log("[SW] Notification click received.");

  event.notification.close();

  if (event.action === "explore") {
    event.waitUntil(clients.openWindow("/"));
  } else if (event.action === "close") {
    // Just close the notification
    return;
  } else {
    // Default action - open the app
    event.waitUntil(
      clients.matchAll().then((clientList) => {
        for (const client of clientList) {
          if (client.url === "/" && "focus" in client) {
            return client.focus();
          }
        }
        if (clients.openWindow) {
          return clients.openWindow("/");
        }
      })
    );
  }
});

// Handle background login sync
async function handleBackgroundLogin() {
  try {
    // Implement your background login logic here
    console.log("[SW] Handling background login sync");

    // Example: retry failed login attempts
    const failedLogins = await getFailedLogins();

    for (const login of failedLogins) {
      try {
        await retryLogin(login);
        await removeFailedLogin(login.id);
      } catch (error) {
        console.error("[SW] Failed to retry login:", error);
      }
    }
  } catch (error) {
    console.error("[SW] Background sync failed:", error);
  }
}

// Helper functions for background sync
async function getFailedLogins() {
  // Implement logic to get failed login attempts from IndexedDB
  return [];
}

async function retryLogin(loginData) {
  // Implement retry login logic
  const response = await fetch("/api/auth/login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(loginData),
  });

  if (!response.ok) {
    throw new Error("Login retry failed");
  }

  return response.json();
}

async function removeFailedLogin(loginId) {
  // Implement logic to remove successful login from IndexedDB
  console.log("[SW] Removing failed login:", loginId);
}

// Message handling between SW and main thread
self.addEventListener("message", (event) => {
  console.log("[SW] Message received:", event.data);

  if (event.data && event.data.type === "SKIP_WAITING") {
    self.skipWaiting();
  }

  if (event.data && event.data.type === "GET_VERSION") {
    event.ports[0].postMessage({ version: CACHE_NAME });
  }
});

// Update available notification
self.addEventListener("message", (event) => {
  if (event.data.action === "skipWaiting") {
    self.skipWaiting();
  }
});
