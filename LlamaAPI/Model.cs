using LLama;
using LLama.Common;

namespace LlamaAPI
{
    public class Model
    {
        ChatSession session;
        ModelParams parameters;
        LLamaWeights model;
        LLamaContext context;
        LLama.InteractiveExecutor ex;
        public Model()
        {

            // Load a model
            parameters = new ModelParams("C:/Users/cablu/Dev/Repos/LlamaAPI/llms/llama-2-7b-guanaco-qlora.Q5_K_M.gguf")
            {
                ContextSize = 1024,
                Seed = 1337,
                GpuLayerCount = 5
            };
            model = LLamaWeights.LoadFromFile(parameters);
            context = model.CreateContext(parameters);
            ex = new InteractiveExecutor(context);
            session = new ChatSession(ex);
        }

        public String getResponse(string input)
        {

            String resp = "";

            var prompt = input + "\n";

                foreach (var text in session.Chat(prompt, new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "User:" } }))
                {
                    resp += text;
                }

            return resp;
        }
    }
}

