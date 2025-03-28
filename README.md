# TaskCLI - Simple Task Tracker  

TaskCLI is a lightweight command-line task tracker built in **C#** without any third-party libraries. It helps you manage your tasks efficiently by allowing you to add, update, delete, and track their progress. All tasks are stored in a **JSON** file in the current directory for easy persistence.

---

## 🚀 Features  

✔ **Add tasks** with descriptions  
✔ **Update and delete** tasks  
✔ **Mark tasks** as `todo`, `in-progress`, or `done`  
✔ **List all tasks** or filter by status  
✔ **JSON-based storage** (automatically created if it doesn't exist)  
✔ **Simple and fast** command-line interface  

---

## 📥 Installation  

### 1️⃣ Clone this repository  
```sh
git clone https://github.com/yourusername/taskcli.git
cd taskcli
```

### 2️⃣ Build the project
```sh
dotnet build -c Release
```

### 3️⃣ Run the binary
`On Linux/macOS:`

```sh
./bin/Release/net7.0/taskcli
```

`On Windows:`

```powershell
bin\Release\net7.0\taskcli.exe
```

### 4️⃣ (Optional) Install globally
`On Linux/macOS:`

```sh
sudo mv bin/Release/net7.0/taskcli /usr/local/bin/taskcli
```

`On Windows:`

```
Add the bin/Release/net7.0/ directory to your system PATH.
```

## 📌 Usage
`Add a New Task`
```sh
taskcli add "Buy groceries"
```
`📝 Output:`
```
Task added successfully (ID: 1)
```

`Update a Task`

```sh
taskcli update 1 "Buy groceries and cook dinner"
```

`Delete a Task`

```sh
taskcli delete 1
```

`Mark a Task as In Progress`

```sh
taskcli mark-in-progress 1
```

`Mark a Task as Done`

```sh
taskcli mark-done 1
```

`List All Tasks`

```sh
taskcli list
```

`List Tasks by Status`

```sh
taskcli list done
taskcli list todo
taskcli list in-progress
```

## 📂 Task Structure (Stored in tasks.json)

Each task is stored with the following properties:

```json
{
  "tasks": [
    {
      "id": 1,
      "description": "Buy groceries",
      "status": "Todo",
      "createdAt": "2025-03-28T14:00:00Z",
      "updatedAt": "2025-03-28T14:00:00Z"
    }
  ]
}
```

## ⚠️ Error Handling
If a task ID does not exist, an appropriate error message is shown. There is also a log file named `taskcli-error-log.log` that is created in the root folder (same as the cli binary).

## 📜 License
This project is licensed under the MIT License.

**Enjoy using TaskCLI to manage your tasks efficiently! 💻🎯**
