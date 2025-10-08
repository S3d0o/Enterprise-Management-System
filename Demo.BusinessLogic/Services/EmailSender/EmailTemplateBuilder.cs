namespace Demo.BusinessLogic.Services.EmailSender
{
    public static class EmailTemplateBuilder
    {
        public static string BuildResetPasswordEmail(string email, string resetUrl)
        {
            return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Reset Your Password</title>
    <style>
        body {{
            font-family: 'Segoe UI', Arial, sans-serif;
            background-color: #f4f4f7;
            color: #333333;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 30px auto;
            background: #ffffff;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }}
        .header {{
            background-color: #0078D7;
            color: white;
            text-align: center;
            padding: 25px 0;
        }}
        .header h1 {{
            margin: 0;
            font-size: 24px;
        }}
        .content {{
            padding: 30px;
            text-align: center;
        }}
        .content h2 {{
            color: #333;
        }}
        .content p {{
            font-size: 16px;
            line-height: 1.5;
            margin: 20px 0;
        }}
        .btn {{
            display: inline-block;
            background-color: #0078D7;
            color: white !important;
            text-decoration: none;
            padding: 12px 25px;
            border-radius: 8px;
            font-weight: bold;
        }}
        .btn:hover {{
            background-color: #005fa3;
        }}
        .footer {{
            background-color: #f4f4f7;
            color: #999;
            text-align: center;
            font-size: 13px;
            padding: 15px;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Reset Your Password</h1>
        </div>
        <div class='content'>
            <h2>Hello {email},</h2>
            <p>We received a request to reset your password. Click the button below to create a new one.</p>
            <p>
                <a href='{resetUrl}' class='btn'>Reset Password</a>
            </p>
            <p>If you didn’t request this, please ignore this email.</p>
        </div>
        <div class='footer'>
            &copy; {DateTime.Now.Year} Demo Application. All rights reserved.
        </div>
    </div>
</body>
</html>";
        }
    }
}
