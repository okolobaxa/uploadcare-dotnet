using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Uploadcare.Models;

namespace Uploadcare.Clients
{
    public interface IFilesClient
    {
        /// <summary>
        /// Requests file data.
        /// </summary>
        /// <param name="fileId"> File UUID </param>
        /// <returns> File resource </returns>
        Task<UploadcareFile> GetAsync(string fileId);

        /// <summary>
        /// Requests file data as Stream
        /// </summary>
        /// <param name="fileId"> File UUID </param>
        /// <returns> File resource </returns>
        Task<Stream> GetStreamAsync(string fileId);

        /// <summary>
        /// Marks a file as deleted.
        /// </summary>
        /// <param name="fileId"> File UUID </param>
        Task DeleteAsync(string fileId);

        /// <summary>
        /// Marks a file as saved.
        /// 
        /// This has to be done for all files you want to keep. Unsaved files are eventually purged.
        /// </summary>
        /// <param name="fileId"> File UUID </param>
        /// <returns> File resource </returns>
        Task<UploadcareFile> StoreAsync(string fileId);

        /// <summary>
        /// Copying uploaded files to a specified storage
        /// </summary>
        /// <param name="source"> A CDN URL or just UUID of a file subjected to copy </param>
        /// <param name="store"> true to store files while copying. If stored, files won’t be automatically deleted after a 24-hour period. false to not store files, default. </param>
        /// <param name="makePublic"> Applicable to custom storage only. true to make copied files available via public links, false to reverse the behavior. </param>
        /// <returns> UUID of new File resource </returns>
        Task<string> CopyAsync(string source, bool store = false, bool makePublic = false);

        /// <summary>
        /// Copying uploaded files to a specified storage
        /// </summary>
        /// <param name="source"> A CDN URL or just UUID of a file subjected to copy </param>
        /// <param name="target"> Identifies a custom storage name related to your project. Implies you are copying a file to a specified custom storage </param>
        /// <param name="store"> true to store files while copying. If stored, files won’t be automatically deleted after a 24-hour period. false to not store files, default. </param>
        /// <param name="makePublic"> Applicable to custom storage only. true to make copied files available via public links, false to reverse the behavior. </param>
        /// <param name="pattern"> Applies to custom storage usage scenario only. The parameter is used to specify file names Uploadcare passes to a custom storage. </param>
        /// <returns> UUID of new File resource </returns>
        Task<string> CopyAsync(string source, string target, bool store = false, bool makePublic = false, string pattern = null);

        /// <summary>
        /// Batch file storing
        /// 
        /// Up to 100 UUIDs are supported per request.
        /// </summary>
        /// <param name="fileIds"> Array of file UUIDs </param>
        /// <returns> Tuple of successfully stored files and errors </returns>
        Task<Tuple<IEnumerable<UploadcareFile>, IReadOnlyDictionary<string, string>>> StoreAsync(IReadOnlyCollection<string> fileIds);

        /// <summary>
        /// Batch file delete
        /// 
        /// Up to 100 UUIDs are supported per request.
        /// </summary>
        /// <param name="fileIds"> Array of file UUIDs </param>
        /// <returns> Tuple of successfully deleted files and errors </returns>
        Task<Tuple<IEnumerable<UploadcareFile>, IReadOnlyDictionary<string, string>>> DeleteAsync(IReadOnlyCollection<string> fileIds);

        /// <summary>
        /// Begins to build a request for uploaded files for the current account.
        /// </summary>
        /// <returns> File resource request builder </returns> 
        FilesQueryBuilder GetFiles();
    }
}