using Blog.Domain.Enums;
using BuildingBlocks.Encryption;

namespace Blog.Infrastructure.Data.Extensions
{
    public class InitialData
    {
        public static IEnumerable<User> Users =>
            new List<User>
            {
                User.Create(UserId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")), "vasim","sunni","vasim@gmail.com", BCryptEncryptor.HashPassword("vasim#123"), UserRole.Admin),
                User.Create(UserId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")), "john","deo","john@gmail.com",BCryptEncryptor.HashPassword("vasim#123"), UserRole.Blogger)
            };

        public static IEnumerable<Category> Categories =>
            new List<Category>
            {
                Category.Create(CategoryId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), "Artificial Intelligence"),
                Category.Create(CategoryId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), "Robotics"),
                Category.Create(CategoryId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), "Data Science"),
                Category.Create(CategoryId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), "Programming")
            };

        public static IEnumerable<Article> ArticlesWithCategories
        {
            get
            {
                var article1 = Article.Create(
                            ArticleId.Of(Guid.NewGuid()),
                            "The Transformative Power of Artificial Intelligence",
                            "<h1>The Transformative Power of Artificial Intelligence</h1>\r\n    <p>Artificial Intelligence (AI) is no longer a futuristic concept; it's a dynamic field transforming various sectors of society. From healthcare to finance, education to entertainment, AI's impact is profound and far-reaching.</p>\r\n\r\n    <h2>What is Artificial Intelligence?</h2>\r\n    <p>AI simulates human intelligence in machines, enabling them to think and learn like humans. These systems can perform tasks such as visual perception, speech recognition, decision-making, and language translation. AI is classified into narrow AI, designed for specific tasks like voice assistants, and general AI, which can perform any intellectual task a human can do.</p>\r\n\r\n    <h2>The Evolution of AI</h2>\r\n    <p>AI's journey began in the mid-20th century with early algorithms and computing machines. It gained momentum in the 1980s and 1990s with advancements in machine learning. The advent of big data and increased computing power further accelerated AI's growth, leading to the development of deep learning and neural networks, enabling unprecedented accuracy and efficiency.</p>\r\n\r\n    <h2>AI in Healthcare</h2>\r\n    <p>One of the most significant impacts of AI is in healthcare. AI is revolutionizing diagnostics, personalized medicine, and patient care by analyzing vast amounts of data to detect patterns and make accurate predictions. This leads to early disease detection, better treatment plans, and improved patient outcomes.</p>\r\n\r\n    <h2>Conclusion</h2>\r\n    <p>Artificial Intelligence is a transformative force reshaping our world. As AI continues to evolve, its applications will expand, offering new opportunities and challenges. Embracing AI's potential while addressing ethical considerations will be crucial for a future where humans and machines work together harmoniously.</p>"
                            );
                article1.AddCategory(CategoryId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")));


                var article2 = Article.Create(
                            ArticleId.Of(Guid.NewGuid()),
                            "The Rise of Robotics: Transforming the Future",
                            "<h1>The Rise of Robotics: Transforming the Future</h1>\r\n    <p>Robotics is a field that combines engineering, computer science, and artificial intelligence to create machines that can perform tasks autonomously or semi-autonomously. These advanced machines are transforming various industries and everyday life, driving efficiency, innovation, and convenience.</p>\r\n\r\n    <h2>What are Robotics?</h2>\r\n    <p>Robotics involves the design, construction, operation, and use of robots. Robots are programmable machines capable of carrying out complex tasks, often in environments that are hazardous or tedious for humans. They can be classified into various types, including industrial robots, service robots, medical robots, and domestic robots.</p>\r\n\r\n    <h2>The Evolution of Robotics</h2>\r\n    <p>The concept of robots dates back to ancient times, but the modern era of robotics began in the 20th century with the development of early automated machines. The 1960s saw the introduction of industrial robots, which revolutionized manufacturing processes. Since then, advancements in technology, such as sensors, actuators, and artificial intelligence, have propelled the field forward, leading to the creation of highly sophisticated robots.</p>\r\n\r\n    <h2>Robotics in Industry</h2>\r\n    <p>One of the most significant impacts of robotics is in the industrial sector. Robots are widely used in manufacturing to perform tasks such as assembly, welding, and painting with high precision and efficiency. They improve productivity, reduce labor costs, and enhance safety by taking on dangerous tasks. The rise of collaborative robots, or cobots, that work alongside humans is further revolutionizing the workplace.</p>\r\n\r\n    <h2>Robotics in Healthcare</h2>\r\n    <p>In healthcare, robots are transforming patient care, surgery, and rehabilitation. Surgical robots enable minimally invasive procedures with high accuracy, leading to quicker recovery times and better outcomes. Rehabilitation robots assist patients in regaining mobility and strength. Additionally, robots are used for tasks such as medication delivery and patient monitoring, enhancing the overall efficiency of healthcare services.</p>\r\n\r\n    <h2>Conclusion</h2>\r\n    <p>Robotics is a rapidly evolving field with the potential to revolutionize numerous aspects of our lives. As technology continues to advance, robots will become increasingly sophisticated, capable, and integrated into various sectors. Embracing robotics' potential while addressing ethical and societal implications will be crucial for a future where humans and robots coexist harmoniously.</p>"
                            );
                article2.AddCategory(CategoryId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")));


                var article3 = Article.Create(
                            ArticleId.Of(Guid.NewGuid()),
                            "The Intersection of Artificial Intelligence and Programming",
                            "<h1>The Intersection of Artificial Intelligence and Programming</h1>\r\n    <p>Artificial Intelligence (AI) and programming are two dynamic fields that are increasingly converging, leading to groundbreaking advancements and innovations. The integration of AI in programming is transforming how software is developed, optimized, and utilized, creating smarter and more efficient applications.</p>\r\n\r\n    <h2>What is Artificial Intelligence in Programming?</h2>\r\n    <p>AI in programming involves incorporating intelligent algorithms and models into software development processes. This enables programs to learn from data, adapt to new information, and perform tasks that traditionally required human intelligence. These AI-powered programs can automate complex processes, make data-driven decisions, and enhance user experiences.</p>\r\n\r\n    <h2>The Evolution of AI in Programming</h2>\r\n    <p>The journey of integrating AI into programming began with the development of basic algorithms and data structures. Over time, advancements in machine learning, natural language processing, and neural networks have significantly expanded the capabilities of AI in software development. Today, AI is used in various stages of programming, from code generation to debugging and optimization.</p>\r\n\r\n    <h2>Applications of AI in Programming</h2>\r\n    <ul>\r\n        <li><strong>Code Generation and Auto-completion</strong>: AI-powered tools like GitHub Copilot and Tabnine assist developers by suggesting code snippets and completing code automatically. These tools use machine learning models trained on vast codebases to provide accurate and context-aware suggestions.</li>\r\n        <li><strong>Debugging and Testing</strong>: AI can identify bugs and vulnerabilities in code more efficiently than traditional methods. Tools like DeepCode and AI-powered testing frameworks analyze code to detect issues, predict potential errors, and suggest fixes, reducing development time and improving code quality.</li>\r\n        <li><strong>Optimization and Performance Tuning</strong>: AI algorithms can analyze software performance and optimize code for better efficiency. Techniques such as genetic algorithms and reinforcement learning are used to fine-tune parameters, optimize resource usage, and enhance overall performance.</li>\r\n        <li><strong>Natural Language Processing (NLP)</strong>: NLP enables computers to understand and generate human language. AI-powered chatbots, virtual assistants, and automated documentation tools use NLP to interact with users, answer queries, and generate content based on natural language inputs.</li>\r\n    </ul>\r\n\r\n    <h2>The Future of AI in Programming</h2>\r\n    <p>The integration of AI in programming is poised to grow even further, with advancements in AI research and development. Future trends may include more sophisticated AI-driven development environments, enhanced collaboration between human developers and AI, and the creation of autonomous software systems that can learn and evolve over time.</p>\r\n\r\n    <h2>Conclusion</h2>\r\n    <p>The intersection of AI and programming is transforming the software development landscape. By leveraging AI, programmers can create smarter, more efficient, and innovative applications. Embracing this synergy will be crucial for staying ahead in the rapidly evolving tech industry, as AI continues to drive the future of programming.</p>"
                            );
                article3.AddCategory(CategoryId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")));
                article3.AddCategory(CategoryId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")));


                return new List<Article> { article1, article2, article3 };
            }
        }
    }
}
