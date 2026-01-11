temp = 0
for i in range(10):
    for j in range(10):
        if j == temp or j == 9 - temp:
            print("0", end = "")
        else:
            print("1", end = "")
    print()
    temp += 1