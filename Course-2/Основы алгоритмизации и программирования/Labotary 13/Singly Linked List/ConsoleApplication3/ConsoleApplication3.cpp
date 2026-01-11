//Создать в динамической памяти односвязный 
//линейный список.Определить сколько
//различных значений содержится в списке.

#include <iostream>
#include <memory>
#include <string>

using namespace std;

template<typename T>
class List {
public:

	List() {
		size = 0;
		head = nullptr;
	}

	~List() {
		clear();
	}

	List(const List& other) {
		size = 0;
		head = nullptr;
		Node<T>* current = other.head;
		while (current != nullptr) {
			this->push_back(current->data);
			current = current->pNext;
		}
	}

	T& operator[](const int index)
	{
		if (index < 0 || index >= size) {
			throw out_of_range("Index out of range");
		}

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

	void push_back(T data)
	{
		if (head == nullptr) {
			head = new Node<T>(data);
		}
		else {
			Node<T>* current = this->head;
			while (current->pNext != nullptr)
			{
				current = current->pNext;
			}
			current->pNext = new Node<T>(data);
		}
		size++;
	}

	void push_front(T data)
	{
		head = new Node<T>(data, head);
		size++;
	}

	void pop_front()
	{
		Node<T>* temp = head;
		head = head->pNext;
		delete temp;
		size--;
	}

	void pop_back()
	{
		removeAt(size - 1);
	}

	void clear()
	{
		while (size) {
			pop_front();
		}
	}

	void insert(T data, int index)
	{
		if (index == 0) {
			push_front(data);
		}

		else {
			Node<T>* previous = this->head;

			for (int i = 0; i < index - 1; i++)
			{
				previous = previous->pNext;
			}

			Node<T>* newNode = new Node<T>(data, previous->pNext);
			previous->pNext = newNode;

			size++;
		}
	}

	void removeAt(int index)
	{
		if (index == 0) {
			pop_front();
		}
		else {
			Node<T>* previous = this->head;

			for (int i = 0; i < index - 1; i++)
			{
				previous = previous->pNext;
			}

			Node<T>* toDelete = previous->pNext;
			previous->pNext = toDelete->pNext;
			delete toDelete;
			size--;
		}
	}

	int getSize() { return  size; }

	int getUniqueElementCount() {
		int uniqueEl = 0;
		for (size_t i = 0; i < this->getSize(); i++)
		{
			bool isUnique = true;
			for (size_t j = 0; j < i; j++)
			{
				if ((*this)[i] == (*this)[j]) {
					isUnique = false;
					break;
				}
			}
			if (isUnique) {
				uniqueEl++;
			}
		}
		cout << this;
		return uniqueEl;
	}

private:
	template<typename T>
	struct Node {
		Node(T data = T(), Node* pNext = nullptr)
		{
			this->data = data;
			this->pNext = pNext;
		}
		Node* pNext;
		T data;
	};

	int size;
	Node <T> *head;
};

int main()
{
	List<int> list;

	int n = 0;
	int temp;
	cin >> n;
	for (size_t i = 0; i < n; i++)
	{
		cin >> temp;
		list.push_back(temp);
	}

	return 0;
}
