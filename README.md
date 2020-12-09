## Telegram Bot Web Hook ASP.NET 

### About
This is the test project for a telegram bot, based on the .NET Core framework.

### How to run bot

1) Find telegram bot named **"@botfarther"**.  
2) To create a new bot type **“/newbot”**.
3) Follow instructions he given and create a new name to your bot.
4) After creating the bot, you will see a new **API token** generated for it.
5) Replace in the appsettings.json with the token that belongs to your bot.
`"BotConfiguration": {  
"BotToken": "<BotToken>"  
}`
6) Install ngrock from this page ngrok - [download](https://ngrok.com/download).  
Ngrok gives you the opportunity to access your local machine from a temporary subdomain provided by ngrok. 
This domain can later send to the telegram API as URL for the webhook. 
7) Open **ngrok.exe** and type in console: `start ngrok on port 8443`  
8) Set Webhook.  
From ngrok you get an URL to your local server. It’s important to use the https one. 
You can post this url als form-data (key: url, value: https://yoursubdomain.ngrok.io/api/update) to the telegram api. 
https://api.telegram.org/botYourBotToken/setWebhook Be aware of the bot prefix in front of your bot token in the URL.
9) Start the Bot in a local instance. Check if the port of the application matches the port on which ngrok is running. 
10) Now your bot should answer every message you send to it.

### Commands

**Menu**:  
**Api** - get info about weather, search articles on wikipedia.org and search videos on youtube;  
**Roll** - random number from 1 to 100;  
**Voice** - set voice answer setting;  
**Train Bot** - add or remove question or answers;  
**Save data** - save dialog bot data and chat settings;

#### TelegramBot.Example: https://github.com/TelegramBots/telegram.bot.examples/tree/master/Telegram.Bot.Examples.WebHook
