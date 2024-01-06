using Discord;
using Discord.Net;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Progam {
    private static DiscordSocketClient client;

    static async Task Main(string[] args)
    {

        // .env
        IConfiguration config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();

        var token = config["DiscordToken"];

        // bot start
        client = new DiscordSocketClient();

        //
        client.Ready += ReadyAsync;
        client.MessageReceived += MessageReceivedAsync;
        client.SlashCommandExecuted += SlashCcommandHandler;
        //

        //
        await client.LoginAsync(Discord.TokenType.Bot, token);
        await client.StartAsync();
        //
       


        await Task.Delay(-1);
    }

    private static async Task MessageReceivedAsync(SocketMessage message)
    {
        if(message.Author.IsBot)
        {
            return;
        }

        if (message.Content.ToLower().Contains("!ola"))
        {

            await message.Channel.SendMessageAsync("boa");
        }
    }

    private static async Task ReadyAsync()
    {

        //
        var guild = client.GetGuild(896940870341890059);
        var guildCommand = new SlashCommandBuilder();
        guildCommand.WithName("firs-command");
        guildCommand.WithDescription("This is my first guild slash command!");
        //

        var globalCommand = new SlashCommandBuilder();
        globalCommand.WithName("first-global-command");
        globalCommand.WithDescription("This is my first global slash command");

        //
        try
        {
            await guild.CreateApplicationCommandAsync(guildCommand.Build());
            await client.CreateGlobalApplicationCommandAsync(globalCommand.Build());
        }
        catch (ApplicationCommandException exception)
        {
            var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
            Console.WriteLine(json);
        }

        /*var channel = guild.GetTextChannel(936372639822401541);
        await channel.SendMessageAsync("Olá, boa noite!");
        Console.WriteLine("Bot está pronto para receber comandos!");*/
    }

    private static async Task SlashCcommandHandler(SocketSlashCommand command)
    {
        using (var clientHTTP = new HttpClient())
        {
            const string waifu = "https://api.waifu.pics/sfw/waifu";
            var url = waifu;
            HttpResponseMessage response = await clientHTTP.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Lê o conteúdo da resposta como uma string
                string content = await response.Content.ReadAsStringAsync();

                // Exibe o conteúdo da resposta
                JObject obj = JObject.Parse(content);
                string _url = (string)obj["url"];
                Console.WriteLine(_url);

                //
                await command.RespondAsync($"{_url}");
            }
            else
            {
                // Se a requisição falhou, exibe o status code
                Console.WriteLine($"Falha na requisição. Status Code: {response.StatusCode}");
            }
        }
    }
}
