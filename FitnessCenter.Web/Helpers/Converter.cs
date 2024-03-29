﻿using Microsoft.AspNetCore.Http;
using Vereyon.Web;

namespace FitnessCenter.Data.Entities
{
    public static class FormFileExtensions
    {

        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}