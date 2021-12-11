# Socket library
import socket
import time
from datetime import datetime

# Speech recognition library
import speech_recognition as sr
# Google Text To Speech: TEXT -> SPEECH
from gtts import gTTS
import playsound
import os


str_count = 0

def reply(text, a):
    tts = gTTS(text = text, lang = "en")
    filename = "text"+ str(str_count) + ".mp3"
    str_count += 1



# Server Socket
HOST = "127.0.0.1"
PORT = 8000

server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server_socket.bind((HOST, PORT))
server_socket.listen()

client_socket, addr = server_socket.accept()

print("Conneted by", addr)

k = 0

while k < 100:
    msg = "Hello " + str(k)
    client_socket.sendall(msg.encode())
    print("COMPLETE SENDING", str(k))
    k += 1
    time.sleep(2)

client_socket.close()
server_socket.close()