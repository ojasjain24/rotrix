from unittest import result
import cv2
import time
import mediapipe as mp
import socket
import time

# Main Code
# Python file that detects the hands and gestures using media pipe.
# Acts as a client and sends the data to the C# server using sockets.

pre_frame_time = 0
new_frame_time = 0
font = cv2.FONT_HERSHEY_SIMPLEX

mpHands = mp.solutions.hands
hands = mpHands.Hands()
mpDraw = mp.solutions.drawing_utils

cam = cv2.VideoCapture(0)

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

UDP_IP = "127.0.0.1"
UDP_PORT = 8000

while True:
    sussess, image = cam.read()
    image = cv2.flip(image, 1)

    result = hands.process(image)
    # print(result.multi_hand_landmarks)
    image_height, image_width, c = image.shape

    if result.multi_hand_landmarks is not None:

        for handLms in result.multi_hand_landmarks:
            mpDraw.draw_landmarks(image, handLms, mpHands.HAND_CONNECTIONS)
        tipLocation = ""
        for hand_landmarks in result.multi_hand_landmarks:
            tipLocation = {'x': hand_landmarks.landmark[mpHands.HandLandmark.RING_FINGER_TIP].x * image_width/10,
                           'y': hand_landmarks.landmark[mpHands.HandLandmark.RING_FINGER_TIP].y * image_height/20,
                           'z': hand_landmarks.landmark[mpHands.HandLandmark.RING_FINGER_TIP].z * 10,
                           'time': time.time()*1000.0
                           }

        sock.sendto(str(tipLocation).encode(), (UDP_IP, UDP_PORT))

    # Calculate FPS
    new_frame_time = time.time()
    FPS = 1/(new_frame_time - pre_frame_time)
    pre_frame_time = new_frame_time
    cv2.putText(image, "FPS: "+str(int(FPS)), (0, 40),
                font, 1, (100, 255, 0), 3, cv2.LINE_AA)

    cv2.imshow('Hand Gesture Recognition', image)
    k = cv2.waitKey(1)
    if k % 256 == 27:
        break

cv2.destroyAllWindows()
