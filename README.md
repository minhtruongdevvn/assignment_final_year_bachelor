# Assignment final year bachelor
## Project description:
A well-designed micro services architecture system is expected from this project, including:

### 1. IDENTITY SERVICE

This service is responsible for authentication and authorization across service, or in other words, it is the 
control center and decentralization of the system. It is responsible for distributing the key to the client 
(JSON Web Token) if they give the correct credential. With the key issued, the client can use it to access 
system services (only if the key has the permission). With the identity service, we can control and manage 
services centrally, bringing great efficiency in decentralization across the service.

### 2. TRAINING SERVICE

This service is made to manage the traning process. The service provides most of training operation divided 
for each different role. Operator has access to CRUD skills, skill categories, lecturers, students and classes 
operation. Students can view their attendances, weekly time-table and view enrolled classes, enroll to a 
class. Lecturers can take attendance for students, view classes that they currently assigned, view their 
weekly time-table, verify and update student’s skill score and verify the student profile. The score (that is 
verified) then is used by the assignment system for suggesting and assigning agent, therefore training 
service need to sync changed data to the assignment service. In addition, the service also supports to CRUD 
external institution that is approved by the agency, then the external institution can view, create and 
update their students in the system, verify and update skill score and profile of their student.

### 3. ASSIGNMENT SERVICE

Assignment service is responsible for assigning and suggesting agent. The suggestion is done based on the 
agent’s skill score (this score is verified and provided by the training service). The assignment service 
contains APIs for training service to sync data, CRUD quests (missions), get suggesting agent of a quest, 
show success rate of an agent with a specific quest, assign or remove agent from a quest. The service also 
hosts a web socket for real-time notifying agents when they are assigned to a quest or their quests is 
updated. For predicting the success rate of an agent, the service must contain the logic for training, 
retraining and using the predicting model.

### 4. TRAINING SINGLE PAGE WEB APPLICATION

The application is divided into 3 part, for student, for lecturer and for operator. The application will map 
the operation of the training service into interface that allow user to understand and interact. The 
application will use user's credential to request the key from identity service then attach that key to call 
training service (to authenticate and authorize). Using the single page web application, the application 
aims to improve user experience as it doesn’t need to reload for displaying different information.

### 5. AGENT ASSIGNMENT DESKTOP APPLICATION

This application is used for assigning and suggesting agents. It handles authentication and authorization, 
map the operation of the assignment service into interface that allow user to understand and interact.

### 6. AGENT MOBILE APPLICATION

This application is made for agent; its purpose is to notify the agent about the quest that they are assigned 
into. In addition, the agents can update the process of the quest they are running.
