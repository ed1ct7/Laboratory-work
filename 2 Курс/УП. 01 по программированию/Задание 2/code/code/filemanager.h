#ifndef FILEMANAGER_H
#define FILEMANAGER_H

#include <iostream>
#include <fstream>
#include <filesystem>
#include <string>
#include <vector>
#include <map>
#include <algorithm>
#include <stdexcept>

using namespace std;
using namespace filesystem;

class DirectoryInfo {
public:
    
    explicit DirectoryInfo(const path& dirPath);

    uintmax_t getSize() const { return size; }
    int getFileCount() const { return fileCount; }
    int getDirCount() const { return dirCount; }
    file_time_type getLastWriteTime() const { return lastWriteTime; }
    path getPath() const { return dirPath; }

private:
    path dirPath;
    uintmax_t size = 0;
    int fileCount = 0;
    int dirCount = 0;
    file_time_type lastWriteTime;
};

class FileManager {
public:
    explicit FileManager(size_t N);

    void createDirectories() const;
    void writeDirectoryInfo() const;
    void findDuplicateReadmes() const;

private:
    size_t N;
};

#endif // FILEMANAGER_H