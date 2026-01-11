#include <iostream>
#include <memory>
#include <string>

using namespace std;

template<typename T>
class List {
public:
	//List() {
	//	size = 0;
	//	head = nullptr;
	//}
	/*~List() {
		clear();
	}*/
	void push_back(T data)
	{
		if (head == nullptr) {
			head = new Node<T>(data);
		}
		else {
			Node<T> * current = this->head;

			while (current->pNext != nullptr)
			{
				current = current->pNext;
			}

			current->pNext = new Node<T>(data);
		}
		size++;
	}
	//void pop_front()//
	//{
	//	Node<T>*temp = head;
	//	head = head->pNext;
	//	delete temp;
	//	size--;
	//}
	/*void clear()
	{
		while (size) {
			pop_front();
		}
	}*/
	//int getSize() {return  size;}

	//void push_front(T data)
	//{
	//	head = new Node<T>(data, head);
	//	size++;
	//}
	void insert(T data, int index)
	{
		if (index == 0) {
			push_front(data)
		}

		else {

			Node<T>* previous = this->head;

			for (int i = 0; i < index - 1; i++)
			{
				previous = previous->pNext;
			}

			Node<T>* newNode = new Node<T>(data, prebious->pNext);
			previous->pNext = newNode;

			size++;
		}
	}
	void removeAt(int index)
	{
		if (index == 0) {
			pop_front()
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
	void pop_back(T data)
	{
		removeAt(size - 1)
	}
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
		Node(T data = T(), Node *pNext = nullptr)
		{
			this->data = data;
			this->pNext = pNext;
		}
		Node *pNext;
		T data;
	};

	int size;

	Node <T> *head;
};

int main()
{
	return 0;
}
