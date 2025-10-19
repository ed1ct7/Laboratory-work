#include <iostream>
#include <memory>
#include <string>

using namespace std;

template<typename T>
class List {
public:

	T& operator[](const int index)
	{
		int counter = 0;
		Node<T>* current = this->head;

		while (current != nullptr)
		{
			if (counter == index) {
				return current->data;
			}
			current = current->pNext;
			counter++;
		}
	}

private:

	template<typename T>
	class Node {
	public:
		Node(T data = T(), Node* pNext = nullptr)
		{
			this->data = data;
			this->pNext = pNext;
		}
		Node* pNext;
		T data;
	};

	int size;

	Node <T>* head;
};

int main()
{
	return 0;
}
