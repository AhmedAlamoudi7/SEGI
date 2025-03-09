using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SEGI.WEB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGI.Services.RepositoryServices
{
    public class RepositoryService : IRepositoryService
    {
        private readonly ApplicationDbContext _context;
        public RepositoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Guid IntToGuid(int value)
        {
            // Create a GUID using a specific part (int) in combination with other random bytes
            byte[] bytes = Guid.NewGuid().ToByteArray();

            // Use the first 4 bytes for the int (other parts remain random)
            BitConverter.GetBytes(value).CopyTo(bytes, 0);

            return new Guid(bytes);
        }
        public int GuidToInt(Guid guid)
        {
            // Get the bytes of the GUID
            byte[] bytes = guid.ToByteArray();

            // Take the first 4 bytes and convert them to an integer
            return BitConverter.ToInt32(bytes, 0);
        }


        /// <summary>
        /// Truncate a specific table in the database.
        /// </summary>
        /// <param name="tableName">The name of the table to truncate.</param>
        public async Task TruncateTableAsync(string tableName)
        {
            // Ensure the table name is not empty or null
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("Table name cannot be null or empty.", nameof(tableName));
            }
            // Execute the TRUNCATE command
            await _context.Database.ExecuteSqlRawAsync($"DELETE FROM {tableName};");
        }
    }
}
