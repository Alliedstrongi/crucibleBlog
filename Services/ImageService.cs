﻿using System.Linq.Expressions;
using crucibleBlog.Services.Interfaces;

namespace crucibleBlog.Services
{
    public class ImageService : IImageService
    {


        private readonly string? _defaultUserImage = "/img/Default.jpg";
        private readonly string? _defaultBlogImage = "/img/Blog/Blog_CodeTag.jpg";
        private readonly string? _defaultCategoryImage = "/img/Default.png";
        //Set up default blog and default Category and default 

        public string? ConvertByteArrayToFile(byte[]? fileData, string? extension, int defaultImage)
        {
            if (fileData == null || fileData.Length == 0)
            {
                switch (defaultImage)
                {
                    //Return the Defualt User Image if the value is 1
                    case 1: return _defaultUserImage;
                    //Return the Defualt Blog Image if the value is 2
                    case 2: return _defaultBlogImage;
                    //Return the Defualt Category Image if the value is 3
                    case 3: return _defaultCategoryImage;
                }
            }
            try
            {
                string? imageBase64Data = Convert.ToBase64String(fileData);
                imageBase64Data = string.Format($"data:{extension};base64,{imageBase64Data}");

                return imageBase64Data;
            }
            catch (Exception)

            {
                throw;
            }

        }

        public async Task<byte[]> ConvertFileToByteArrayAsync(IFormFile? file)
        {
            try
            {
                using MemoryStream memoryStream = new MemoryStream();
                await file!.CopyToAsync(memoryStream);
                byte[] byteFile = memoryStream.ToArray();
                memoryStream.Close();

                return byteFile;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
