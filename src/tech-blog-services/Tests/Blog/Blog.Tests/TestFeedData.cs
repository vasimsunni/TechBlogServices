namespace Blog.IntegrationTests
{
    public static class TestFeedData
    {
        public static ArticleRequestModel Article = new()
        {
            Id= new Guid("256B0811-44A5-47A5-85E4-602FF9F275BF"),
            Title = "Programming and Computer Algorithms: The Building Blocks of Modern Technology",
            Body = "<h1>Programming and Computer Algorithms: The Building Blocks of Modern Technology</h1>\r\n    <p>Programming and computer algorithms are fundamental components of modern technology, driving innovation and efficiency in various fields. Understanding these concepts is crucial for developing robust software solutions and solving complex problems.</p>\r\n\r\n    <h2>What is Programming?</h2>\r\n    <p>Programming is the process of creating instructions for a computer to execute. These instructions, written in a programming language, tell the computer how to perform specific tasks. Programming languages range from low-level languages like Assembly to high-level languages like Python, Java, and C++.</p>\r\n\r\n    <h2>What are Computer Algorithms?</h2>\r\n    <p>Algorithms are step-by-step procedures or formulas for solving problems. In computing, an algorithm is a set of instructions designed to perform a specific task. Algorithms are the heart of programming, providing the logic that drives software applications. They can range from simple operations like sorting numbers to complex processes like machine learning and data analysis.</p>\r\n\r\n    <h2>The Evolution of Programming and Algorithms</h2>\r\n    <p>The evolution of programming and algorithms began with early computing machines and has progressed through several generations of programming languages and algorithmic advancements. Early computers used machine code and assembly language, which evolved into higher-level languages to simplify programming and improve efficiency. Simultaneously, algorithm development has grown from basic mathematical operations to sophisticated algorithms that underpin artificial intelligence and big data analytics.</p>\r\n\r\n    <h2>Key Concepts in Programming and Algorithms</h2>\r\n    <ul>\r\n        <li><strong>Data Structures</strong>: Data structures organize and store data efficiently. Common data structures include arrays, linked lists, stacks, queues, trees, and graphs. Choosing the right data structure is essential for optimizing performance and resource usage.</li>\r\n        <li><strong>Algorithm Design</strong>: Designing an algorithm involves defining the problem, breaking it down into smaller tasks, and creating a step-by-step solution. Techniques such as divide and conquer, dynamic programming, and greedy algorithms are commonly used to develop efficient solutions.</li>\r\n        <li><strong>Complexity Analysis</strong>: Understanding the efficiency of an algorithm is crucial for assessing its performance. Complexity analysis evaluates the time and space requirements of an algorithm, often using Big O notation to describe its behavior as the input size grows.</li>\r\n        <li><strong>Recursion</strong>: Recursion is a technique where a function calls itself to solve smaller instances of the same problem. It's a powerful tool for simplifying code and solving problems that have a repetitive structure.</li>\r\n        <li><strong>Sorting and Searching</strong>: Sorting and searching algorithms are fundamental in computer science. Common sorting algorithms include QuickSort, MergeSort, and BubbleSort, while searching algorithms include Binary Search and Linear Search.</li>\r\n    </ul>\r\n\r\n    <h2>Applications of Programming and Algorithms</h2>\r\n    <p>Programming and algorithms are applied in various domains, including:</p>\r\n    <ul>\r\n        <li><strong>Software Development</strong>: Creating applications, websites, and systems software.</li>\r\n        <li><strong>Data Analysis</strong>: Processing and analyzing large datasets to extract valuable insights.</li>\r\n        <li><strong>Artificial Intelligence</strong>: Developing intelligent systems that can learn and make decisions.</li>\r\n        <li><strong>Cybersecurity</strong>: Designing secure systems and detecting vulnerabilities.</li>\r\n        <li><strong>Game Development</strong>: Creating interactive and immersive gaming experiences.</li>\r\n    </ul>\r\n\r\n    <h2>Conclusion</h2>\r\n    <p>Programming and computer algorithms are the building blocks of modern technology. They enable the development of innovative solutions to complex problems, driving progress in numerous fields. Understanding these concepts is essential for anyone looking to contribute to the ever-evolving landscape of technology.</p>",
            Categories = [
                new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27"),
                new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")
            ]
        };


        public static CategoryRequestModel Category = new()
        {
            Id = new Guid("5334C996-8457-4CF0-815C-ED2B77C4FF61"),
            CategoryName = "Artificial Intelligence (Update)"
        };
    }

    public class ArticleRequestModel
    {
        public Guid Id{ get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public List<Guid> Categories { get; set; }
    }

    public class CategoryRequestModel
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }    }
}
