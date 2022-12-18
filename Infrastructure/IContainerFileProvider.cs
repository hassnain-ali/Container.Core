namespace AspNetCore.Container.Infrastructure;

/// <summary>
/// 
/// </summary>
public interface IContainerFileProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="paths"></param>
    /// <returns></returns>
    string Combine(params string[] paths);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    void CreateDirectory(string path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    void CreateFile(string path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    void DeleteDirectory(string path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    void DeleteFile(string filePath);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    bool DirectoryExists(string path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sourceDirName"></param>
    /// <param name="destDirName"></param>
    void DirectoryMove(string sourceDirName, string destDirName);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <param name="searchPattern"></param>
    /// <param name="topDirectoryOnly"></param>
    /// <returns></returns>
    IEnumerable<string> EnumerateFiles(string directoryPath, string searchPattern, bool topDirectoryOnly = true);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sourceFileName"></param>
    /// <param name="destFileName"></param>
    /// <param name="overwrite"></param>
    void FileCopy(string sourceFileName, string destFileName, bool overwrite = false);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    bool FileExists(string filePath);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    long FileLength(string path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sourceFileName"></param>
    /// <param name="destFileName"></param>
    void FileMove(string sourceFileName, string destFileName);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="paths"></param>
    /// <returns></returns>
    string GetAbsolutePath(params string[] paths);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    DirectorySecurity GetAccessControl(string path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    DateTime GetCreationTime(string path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="searchPattern"></param>
    /// <param name="topDirectoryOnly"></param>
    /// <returns></returns>
    string[] GetDirectories(string path, string searchPattern = "", bool topDirectoryOnly = true);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    string? GetDirectoryName(string? path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    string GetDirectoryNameOnly(string path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    string GetFileExtension(string filePath);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="subpath"></param>
    /// <returns></returns>
    IFileInfo GetFileInfo(string subpath);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    string GetFileName(string path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    string GetFileNameWithoutExtension(string filePath);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <param name="searchPattern"></param>
    /// <param name="topDirectoryOnly"></param>
    /// <returns></returns>
    string[] GetFiles(string directoryPath, string searchPattern = "", bool topDirectoryOnly = true);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    DateTime GetLastAccessTime(string path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    DateTime GetLastWriteTime(string path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    DateTime GetLastWriteTimeUtc(string path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <returns></returns>
    string? GetParentDirectory(string directoryPath);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    string? GetVirtualPath(string? path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    bool IsDirectory(string path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    string MapPath(string path);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    byte[] ReadAllBytes(string filePath);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    string ReadAllText(string path, Encoding encoding);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="lastWriteTimeUtc"></param>
    void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="bytes"></param>
    void WriteAllBytes(string filePath, byte[] bytes);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="contents"></param>
    /// <param name="encoding"></param>
    void WriteAllText(string path, string contents, Encoding encoding);
}
