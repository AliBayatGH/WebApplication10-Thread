
using System;
using System.Diagnostics;

namespace WebApplication10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var gcInfo = GC.GetGCMemoryInfo();
            Console.WriteLine($"Heap Hard Limit: {gcInfo.HeapSizeBytes / 1024 / 1024} MB");
            Console.WriteLine($"Total Available Memory: {gcInfo.TotalAvailableMemoryBytes / 1024 / 1024} MB");

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;
            Console.WriteLine("Process priority set to BelowNormal.");

            Process process = Process.GetCurrentProcess();
            // Set processor affinity to use only CPU 0 and CPU 1 (binary 11, i.e., 0x3)
            process.ProcessorAffinity = (IntPtr)0x3;



            // Set thread pool limits
            bool setMaxSuccess = ThreadPool.SetMaxThreads(50, 50); // Max 4 worker and 4 I/O threads
            bool setMinSuccess = ThreadPool.SetMinThreads(2, 2); // Min 2 worker and 2 I/O threads

            Console.WriteLine($"SetMaxThreads Success: {setMaxSuccess}");
            Console.WriteLine($"SetMinThreads Success: {setMinSuccess}");

            ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCompletionPortThreads);
            ThreadPool.GetAvailableThreads(out int availableWorkerThreads, out int availableCompletionPortThreads);
            Console.WriteLine($"Max Worker Threads: {maxWorkerThreads}");
            Console.WriteLine($"Available Worker Threads: {availableWorkerThreads}");
            Console.WriteLine($"Max IO Threads: {maxCompletionPortThreads}");
            Console.WriteLine($"Available IO Threads: {availableCompletionPortThreads}");


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
