# StartTurista - Turista Environment Launcher

A simple Windows Forms application that provides a graphical interface to launch different Turista environments (Production, Staging, Local).

## Features

- **Simple GUI**: Clean interface with 3 colored buttons
- **Environment Selection**: 
  - Production (Blue button)
  - Staging (Orange button) 
  - Local (Green button)
- **Automatic Configuration**: Copies the appropriate .ini file before launching
- **Error Handling**: User-friendly error messages for missing files
- **Single Executable**: Self-contained .exe file

## How it Works

Each button performs these actions:
1. Copy the corresponding .ini file to `C:\ASI\Turista\Daten\turista.ini`
2. Launch `C:\ASI\Turista\Programm\turista-ve.exe`
3. Show success message and close launcher

## Building

### Prerequisites
- .NET 9 SDK
- Windows (for Windows Forms support)

### Build Instructions
1. Run `build.bat` to create the executable
2. The output will be in the `publish` folder as `StartTurista.exe`

### Manual Build
```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o ./publish
```

## File Structure

- **StartTurista.csproj**: Project configuration
- **Program.cs**: Application entry point
- **MainForm.cs**: Main GUI form with button handlers
- **build.bat**: Build script for easy compilation

## Configuration Files Expected

The application expects these files to exist:
- `C:\ASI\Turista\Daten\turista_prod.ini`
- `C:\ASI\Turista\Daten\turista_staging.ini`
- `C:\ASI\Turista\Daten\turista_local.ini`
- `C:\ASI\Turista\Programm\turista-ve.exe`

## Usage

1. Run `StartTurista.exe`
2. Click the desired environment button
3. The application will configure and launch Turista
4. Success message will appear and launcher will close