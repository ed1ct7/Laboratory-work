#include "filemanager.h"
#include <iostream>

int main() {
    try {
        // Get N from user with validation
        size_t N;
        cout << "Enter N (2-12): ";
        cin >> N;
        FileManager fm(N);
        // Task 1:
        fm.createDirectories();
        // Task 2:
        fm.writeDirectoryInfo();
        // Task 3:
        fm.findDuplicateReadmes();
        cout << "All operations completed successfully.\n";
    }
    catch (const exception& e) {
        cerr << "Error: " << e.what() << endl;
        return 1;
    }

    return 0;
}