import sys
import time
import os.path
from Tkinter import *
from naoqi import ALProxy
import almath
import tkMessageBox
f = None
robotIP      = ""
port = ""
motionProxy = None
class App:
    # All UI-building
    def __init__(self, master):
        frame = Frame(master)
        frame.pack()
        self.e = Entry(master, width = 80)
        self.e.pack()
        self.e.delete(0, END)
        self.e.insert(0, "Enter the filepath of the NAO movement file, including the filename.")
        self.f = Entry (master,width = 70)
        self.f.pack()
        self.f.delete(0,END)
        self.f.insert(0,"Enter the IP address of the NAO Robot (default is 192.168.0.102).")
        self.g = Entry(master,width = 45)
        self.g.pack()
        self.g.delete(0,END)
        self.g.insert(0,"Enter the port of the NAO Robot (default is 9559).")
        self.button = Button(frame,text = "Load File", fg = "black", height = 1, width = 60, command = lambda: self.load(self.e.get(),self.f.get(),self.g.get()))
        self.button.pack(side=LEFT)
        self.button = Button(frame, text = "Send File to NAO Robot", fg = "black", height = 1, width = 60, command = NAO)
        self.button.pack(side=RIGHT)
    def load(frame,text1,text2,text3):
        if (os.path.isfile(text1) is True):
            global f
            f = open(text1, 'r')
            tkMessageBox.showinfo("File Loaded","Successfully loaded the file!")
        else: 
            tkMessageBox.showerror("Open file", "Cannot open the file:\n(%s),\n it does not exist" % text1)       
            return
        global robotIP
        robotIP = text2
        global port
        port = text3
def NAO():
    # Init proxies.
    global motionProxy
    global robotIP
    if(robotIP == "" or robotIP == "Enter the IP address of the NAO Robot (default is 192.168.0.102)."): 
        robotIP = "192.168.0.102"
    global port
    if(port == "" or port == "Enter the port of the NAO Robot (default is 9559)."): 
        port = "9559"
    try:
        motionProxy = ALProxy("ALMotion", robotIP, int(port))
    except Exception, e:
        tkMessageBox.showerror("Connection Error", "Could not create proxy to ALMotion, error was: %s " % e)
        return
    if (f is None): return
    else:
        names = ""
        motionProxy.setAngles("RShoulderPitch",-30*almath.TO_RAD,1.0)
        motionProxy.setAngles("LShoulderPitch",-30*almath.TO_RAD,1.0)
        for line in f:
            i = 2
            waittimestring = ""
            while(i < len(line)): 
                waittimestring += line[i]
                i += 1
            if(line[0] == '0' or line[0] == '1'):
                names = "RShoulderPitch"
            else: 
                names = "LShoulderPitch"
            motionProxy.setAngles(names, 90*almath.TO_RAD, 1.0)
            time.sleep(0.75)
            motionProxy.setAngles(names,-30*almath.TO_RAD,1.0)
            time.sleep(float(waittimestring)/1000)
def main():
    #Init UI
    root = Tk()
    app = App(root)
    root.title("NAO Module")
    root.mainloop()
if __name__ == "__main__":
    main()