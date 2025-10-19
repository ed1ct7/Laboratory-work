/*
1) создание в текущем каталоге системы подкаталогов 1\ 2\ 3Е\ N
(число N вводит пользователь, предусмотреть обработку исключени€,
если оно превышает значение 12, меньше 2, отрицательно или
€вл€етс€ нечисловым значением);

2) переход в каталог 2,
создание в нЄм файла с запис€ми путей к  каталогам с 3 до N
и статистических данных этих каталогов;

3) обход всех каталогов
и подкаталогов на диске, поиск файлов с именем README, проверка,
есть ли среди них файлы одного размера(если да, то вывод в файл
FIND.txt в каталоге 1 путей к ним, в противном случае в файл
вывести ЂNOT FINDї).
*/

#include "filemanager.h"
#include <iostream>
#include <fstream>

DirectoryInfo::DirectoryInfo(const path& dirPath) : dirPath(dirPath) {
    if (!exists(dirPath)) {
        throw runtime_error("Directory doesn't exist");
    }
    lastWriteTime = last_write_time(dirPath);
    for (const auto& entry : directory_iterator(dirPath)) {
        try {
            if (entry.is_regular_file()) {
                size += entry.file_size();
                fileCount++;
            }
            else if (entry.is_directory()) {
                dirCount++;
            }
        }
        catch (const filesystem_error&) {
            continue;
        }
    }
}

FileManager::FileManager(size_t N) : N(N) {
    if (N < 2 || N > 12) {
        throw invalid_argument("N must be between 2 and 12");
    }
}

void FileManager::createDirectories() const {
    for (size_t i = 1; i <= N; i++) {
        path dir = to_string(i);
        if (!exists(dir)) {
            create_directory(dir);
        }
    }
}

void FileManager::writeDirectoryInfo() const {
    current_path("2");
    ofstream infoFile("INFO.txt");

    if (!infoFile.is_open()) {
        throw runtime_error("Failed to open INFO.txt");
    }

    for (size_t i = 3; i <= N; i++) {
        path dirPath = "../" + to_string(i);
        DirectoryInfo dirInfo(dirPath);

        infoFile << "Directory: " << dirPath << "\n";
        infoFile << "\tSize: " << dirInfo.getSize() << " bytes\n";
        infoFile << "\tFiles: " << dirInfo.getFileCount() << "\n";
        infoFile << "\tSubdirectories: " << dirInfo.getDirCount() << "\n";
        infoFile << "\tLast modified: " << dirInfo.getLastWriteTime().time_since_epoch().count() << "\n\n";
    }

    current_path("..");
}

void FileManager::findDuplicateReadmes() const {
    map<uintmax_t, path> readmeFiles;

    path rootPath = string(1, 'C') + ":\\";
    try {
        for (const auto& entry : recursive_directory_iterator(rootPath)) {
            if (entry.is_regular_file() && entry.path().filename() == "README.txt") {
                readmeFiles[entry.path(), entry.file_size()];
            }
        }
    }
    catch (const filesystem_error&) {
        cout << "ќшибка";
    }

    ofstream findFile("1/FIND.txt");
    bool foundDuplicates = false;

    for (const auto& [size, path] : readmeFiles) {
        //if (path.size() > 1) {
        //    foundDuplicates = true;
        //    findFile << "README files with size " << size << " bytes:\n";
        //    for (const auto& path : paths) {
        //        findFile << "  " << path << "\n";
        //    }
        //    findFile << "\n";
        //}
    }

    if (!foundDuplicates) {
        findFile << "NOT FIND\n";
    }
}