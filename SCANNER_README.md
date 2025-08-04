# SPJ Barcode Scanner

Scanner barcode untuk aplikasi eSPJ yang menggunakan QuaggaJS library.

## Fitur

- Scanner barcode real-time menggunakan kamera device
- Mendukung berbagai format barcode (Code 128, Code 39, EAN, dll)
- Input manual sebagai alternatif
- Validasi format SPJ
- Responsive design untuk mobile dan desktop
- Sound feedback saat barcode terdeteksi

## Format Barcode yang Didukung

- Code 128
- Code 39
- Code 39 VIN
- EAN-13
- EAN-8
- Code 93

## Cara Penggunaan

### Untuk User (Driver)

1. **Akses Halaman Scanner**

   - Buka aplikasi eSPJ
   - Navigasi ke halaman "Scan SPJ"

2. **Menggunakan Camera Scanner**

   - Klik tombol "Mulai Scan"
   - Izinkan akses kamera saat diminta browser
   - Arahkan kamera ke barcode SPJ
   - Scanner akan otomatis mendeteksi dan menampilkan hasil
   - Klik "Konfirmasi" untuk melanjutkan atau "Scan Ulang" untuk mencoba lagi

3. **Menggunakan Input Manual**
   - Masukkan kode SPJ secara manual di field yang disediakan
   - Klik tombol search untuk memproses

### Browser Requirements

- Chrome 21+
- Firefox 17+
- Safari 11+
- Edge 12+
- Opera 18+

### Permissions

Scanner memerlukan akses kamera. Pastikan:

- Akses kamera diizinkan pada browser
- Halaman diakses melalui HTTPS (untuk production)
- Device memiliki kamera yang berfungsi

## Technical Implementation

### Dependencies

- **QuaggaJS**: Library untuk barcode scanning
- **Lucide Icons**: Untuk iconography
- **Tailwind CSS**: Untuk styling

### File Structure

```
Views/Admin/Transport/SpjDriver/Scan/
├── Index.cshtml                 # Main scanner page
Controllers/SpjDriverController/
├── ScanController.cs            # Backend logic
wwwroot/driver/css/
├── scanner.css                  # Scanner-specific styles
```

### Key Components

1. **BarcodeScanner Class** (JavaScript)

   - Handles camera initialization
   - Manages QuaggaJS configuration
   - Processes scan results
   - Handles UI interactions

2. **ScanController** (C#)
   - Validates scanned codes
   - Processes SPJ lookup
   - Handles error responses

### Configuration

QuaggaJS configuration:

```javascript
{
    inputStream: {
        type: "LiveStream",
        constraints: {
            width: 320,
            height: 240,
            facingMode: "environment" // Use back camera
        }
    },
    decoder: {
        readers: [
            "code_128_reader",
            "code_39_reader",
            "ean_reader",
            // ... more readers
        ]
    }
}
```

## Customization

### Menambah Format Barcode Baru

Edit array `readers` di file Index.cshtml:

```javascript
readers: ["code_128_reader", "your_new_reader_here"];
```

### Mengubah Validasi SPJ

Edit method `ValidateSpjCode` di ScanController.cs:

```csharp
private async Task<SpjData?> ValidateSpjCode(string barcode)
{
    // Your custom validation logic here
}
```

### Styling

Edit file `scanner.css` untuk mengubah appearance scanner.

## Troubleshooting

### Camera Tidak Berfungsi

1. Pastikan browser memiliki akses kamera
2. Cek apakah halaman diakses melalui HTTPS
3. Restart browser jika perlu
4. Cek device permissions

### Barcode Tidak Terdeteksi

1. Pastikan barcode dalam format yang didukung
2. Cek pencahayaan - barcode harus jelas terbaca
3. Jaga jarak optimal (15-30cm dari kamera)
4. Pastikan barcode tidak rusak atau blur

### Performance Issues

1. Tutup aplikasi lain yang menggunakan kamera
2. Gunakan browser yang up-to-date
3. Cek kecepatan internet untuk loading library

## Development Notes

- Library QuaggaJS dimuat dari CDN (dapat diunduh lokal jika perlu)
- Scanner otomatis stop setelah berhasil scan untuk menghemat resource
- Implementasi includes sound feedback dan haptic feedback
- Mobile-first responsive design

## Future Enhancements

- [ ] Support untuk QR Code
- [ ] Batch scanning multiple barcodes
- [ ] Offline scanning capability
- [ ] Advanced barcode validation
- [ ] Scan history
- [ ] Analytics dan reporting
