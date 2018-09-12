using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.TextToSpeech.V1;

namespace ConsoleApp2 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("1: type text manually");
            Console.WriteLine("2: Parse from file");
            var answer = Console.ReadLine();
            switch (answer) {
                case "1":
                    while (true) {
                        Console.Write("Enter Text: ");
                        var text = Console.ReadLine();
                        Console.Write("Enter Filename: ");
                        var filename = Console.ReadLine();

                        synth(text, filename + ".mp3");
                    }
                    break;
                case "2":
                    Console.WriteLine("Reading from input.txt");
                    var count = 0;
                    foreach (string line in File.ReadLines($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\OneDrive\\DaVinci Design\\VO\\GeneratedGCloud\\For video\\input.txt")) {
                        var filename = line.Length > 30 ? line.Substring(0, 30) : line;

                        if (filename != "") {
                            synth(line, $"{filename}.mp3");
                            count++;
                        }
                    }
                    break;
            }
        }

        static void synth(string text, string filename) {
            var client = TextToSpeechClient.Create();

            var input = new SynthesisInput {
                Text = text
            };

            var voice = new VoiceSelectionParams {
                LanguageCode = "en-AU",
                Name = "en-AU-Wavenet-B"
            };

            var config = new AudioConfig {
                AudioEncoding = AudioEncoding.Mp3,
                Pitch = 1,
                SpeakingRate = 0.95
            };

            /*VoiceSelectionParams voice = new VoiceSelectionParams
            {
                LanguageCode = "en-US",
                Name = "en-US-Wavenet-F"
            };

            AudioConfig config = new AudioConfig
            {
                AudioEncoding = AudioEncoding.Mp3,
                Pitch = 1,
                SpeakingRate = 1
            };*/

            var response = client.SynthesizeSpeech(new SynthesizeSpeechRequest {
                Input = input,
                Voice = voice,
                AudioConfig = config
            });

            using (Stream output = File.Create($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\OneDrive\\DaVinci Design\\VO\\GeneratedGCloud\\For video\\" + filename)) {
                response.AudioContent.WriteTo(output);
                Console.WriteLine($"Audio content written to file '{filename}'");
            }
        }
    }
}