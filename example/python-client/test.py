import requests
from  time import sleep
import  math


url = 'http://localhost:81/setcommand'


def setBlock(x, y, z, type):
    "doc"
    print(x, y, z)
    comm = "/setblock " + x.__str__() + " " + y.__str__() + " " + z.__str__() + " " + type
    return requests.post(url, json={"command": comm})


def fill(x1, y1, z1,length, height, width, type):
    "doc"
    print(x1, y1, z1)
    comm = "/fill " + x1.__str__() + " " + y1.__str__() + " " + z1.__str__() + " " + (x1+length).__str__() + " " + (y1+height).__str__() + " " + (z1+width).__str__() + " " + type
    return requests.post(url, json={"command": comm})



def drawCube(x1, y1, z1, l, h, w, type):
    print(fill(x1,  y1,   z1,
                l,    h,  w,
                type).content)


def PointsInCircum(r,n):
    return [(int(math.cos(2*3.14/n*x)*r), int(math.sin(2*3.14/n*x)*r)) for x in range(0,n+1)]


def drawCicle(x1,y1,z1, r,len, type):
    arr = PointsInCircum(r,len)
    for i in range(arr.__len__()):
        setBlock(arr[i][0], y1, arr[i][1],
                   type)

def drawSphere(x1,y1,z1,r, type):
    #По слоям отрисовывам сферу. начинаем снизу. каджый сдвигаемся по диагонали +1

    for i in range(int (r/2)):
        drawCicle(i, 80 -i, i, r-i, 15 * 15, "stone")

def drawCubicSphere(x1,y1,z1,r, type):
    for i in range(r):
        drawCube(x1- int(i/2), y1+i, z1- int(i/2), i, 0, i, type)

#print(PointsInCircum(10,100).__len__())
#drawCube(-20,80,-20,30,20,30,"air")
#drawCicle(0,80,0,15,15*15,"stone")
#drawCicle(1,81,2,14,15*15,"stone")
#drawSphere(0,80,0,10,"grass")
#drawCubicSphere(130,100,130,60,"grass")
sleep(5);
for i in range(100):
    fill(i,60,0,  0,30,0,"diamond_block")
    fill(0, 60, i, 0, 30, 0, "diamond_block")

    fill(i, 60, 100, 0, 30, 0, "diamond_block")
    fill(100, 60, i, 0, 30, 0, "diamond_block")