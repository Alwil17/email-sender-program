# EmailSenderProgram

A simple .NET console application that automatically sends emails to customers.  
Currently supports two email types:
- **Welcome Email**: Sent to new customers (registered within the last 24 hours).
- **Comeback Email**: Sent to customers with no orders (e.g. only on Sundays in production).

## Key Features
- **Modular Design**: Email types are implemented via the `IMailSender` interface.
- **Configurable**: Uses `App.config` to set parameters (SMTP host, voucher code, etc.).
- **Debug vs. Release**: In DEBUG mode, emails are printed to the console instead of being sent.
- **Logging**: (Future implementation) Logging with log4net for monitoring.

## Project Structure
```
EmailSenderProgram/
│
├── Program.cs                // Application entry point  
├── DataLayer.cs              // Mock data for customers and orders 
├── ...                       // other files and folders
│
├── Config/
│   └── ConfigResolver.cs     // Handles All configurations from App.config
│
├── Mail/
│   ├── IMailSender.cs        // Email sender interface  
│   ├── WelcomeMailSender.cs  // Handles welcome emails  
│   └── ComebackMailSender.cs // Handles comeback emails  
│
├── Services/
│   └── MailService.cs        // Responsible for sending emails via SMTP  
│
├── Scheduler/
│   └── Scheduler.cs          // Coordinates which emails to send and when  
│
└── App.config                // App settings (SMTP host, Password, etc.)
```

## Getting Started
1. Update `app.config` with your SMTP and other configuration values.
2. In DEBUG mode, the program prints email details to the console.
3. Build and run the project:
   ```bash
   dotnet run
   ```
4. For production, compile in Release mode to send real emails.

Enjoy and feel free to extend the project by adding more email types!