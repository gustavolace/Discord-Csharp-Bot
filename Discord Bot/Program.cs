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

    private static async Task Main(string[] args)
    {
        Progam program = new Progam();
        await program.StartAsync();
    }

    private async Task StartAsync()
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
        //client.SlashCommandExecuted += SlashCcommandHandler;
        //

        //
        await client.LoginAsync(Discord.TokenType.Bot, token);
        await client.StartAsync();
        //



        await Task.Delay(-1);
    }

    private async Task MessageReceivedAsync(SocketMessage message)
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

    public async Task ReadyAsync()
    {

       /* var globalCommand = new SlashCommandBuilder();
        globalCommand.WithName("choise");
        globalCommand.WithDescription("This is my first global slash command");
        globalCommand.AddOption(new SlashCommandOptionBuilder()
         .WithName("sfw")
         .WithDescription("")
         .WithRequired(true)
         .AddChoice("Hello", 1)); */

        var globalCommand2 = new SlashCommandBuilder()
        .WithName("second")
        .WithDescription("This is my seccond global slash command");

       /* var globalCommand3 = new SlashCommandBuilder()
       .WithName("theree")
       .WithDescription("e  otress tres ")
       .AddOption(new SlashCommandOptionBuilder()
         .WithName("sfw")
         .WithDescription("")
         .WithRequired(true)
         .AddChoice("Hello", 1)); */

        var globalCommand4 = new SlashCommandBuilder()
       .WithName("4")
       .WithDescription("e  otress tres ");


        try
        {
            //await client.CreateGlobalApplicationCommandAsync(globalCommand.Build());
            await client.CreateGlobalApplicationCommandAsync(globalCommand2.Build());
            //await client.CreateGlobalApplicationCommandAsync(globalCommand3.Build());
            await client.CreateGlobalApplicationCommandAsync(globalCommand4.Build());
        }
        catch (ApplicationCommandException exception)
        {
            var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
            Console.WriteLine(json);
        }

        /* try
         {
             await client.Rest.CreateGlobalCommand(globalCommand.Build());
         }
         catch(ApplicationCommandException exception)
         {
             var json = JsonConvert.SerializeObject(exception, Formatting.Indented);
             Console.WriteLine(json);
         }

         */

    }



   /* private static async Task SlashCcommandHandler(SocketSlashCommand command)
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

                var embed = new EmbedBuilder()
                .WithTitle("gostosa")
                .WithImageUrl(_url)
                .Build();

                //
                await command.RespondAsync(embed: embed);
            }
            else
            {
                // Se a requisição falhou, exibe o status code
                Console.WriteLine($"Falha na requisição. Status Code: {response.StatusCode}");
            }
        }
    } 
   */
}
