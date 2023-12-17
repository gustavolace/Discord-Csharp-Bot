using DotNetEnv;
using Discord.WebSocket;


        Env.Load();
        var token = Environment.GetEnvironmentVariable("DiscordToken");
        var client = new DiscordSocketClient();

        await client.LoginAsync(Discord.TokenType.Bot, token);

        await client.StartAsync();

        client.Ready += async () =>
        {
        var guild = client.GetGuild(896940870341890059);
        var channel = guild.GetTextChannel(936372639822401541);

        await channel.SendMessageAsync("Ola boa noite");
        };


        Console.ReadKey();

