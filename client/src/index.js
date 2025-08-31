document.addEventListener('DOMContentLoaded', () => {
  const todolistBtnHtml = `<button id="listTodos">Visa alla uppdrag </button><div id="ShowTodoList"></div>`;
  document.body.insertAdjacentHTML('beforeend', todolistBtnHtml);
  document.getElementById('listTodos').addEventListener('click', listAllTodos);
});

async function listAllTodos() {
  const response = await fetch(`${baseApiUrl}/task`, {
    method: 'GET',
    credentials: 'include',
  });
  const todolistsDiv = document.getElementById('ShowTodoList');
  if (response.ok) {
    const result = await response.json();
    const todolists = result.data;
    todolistsDiv.innerHTML = '<h3>Alla uppdrag:</h3>' + todolists.map(t => `<div>${t.title ?? t.name ?? JSON.stringify(t)}</div>`).join('');
  } else {
    todolistsDiv.innerHTML = '<span style="color:red">Kunde inte hämta ToDoListan.</span>';
  }
}

document.addEventListener('DOMContentLoaded', () => {
  const userBtnHtml = `<button id="listUsers">Visa alla användare</button><div id="userList"></div>`;
  document.body.insertAdjacentHTML('beforeend', userBtnHtml);
  document.getElementById('listUsers').addEventListener('click', listAllUsers);
});

async function listAllUsers() {
  const response = await fetch(`${baseApiUrl}/accounts/ListAllUsers`, {
    method: 'GET',
    credentials: 'include',
  });
  const userListDiv = document.getElementById('userList');
  if (response.ok) {
    const users = await response.json();
    userListDiv.innerHTML = '<h3>Användare:</h3>' + users.map(u => `<div>${u.email} (${u.userName})</div>`).join('');
  } else {
    userListDiv.innerHTML = '<span style="color:red">Kunde inte hämta användare.</span>';
  }
}
// Registreringsformulär
document.addEventListener('DOMContentLoaded', () => {
  const formHtml = `
    <h2>Registrera ny användare</h2>
    <form id="registerForm">
      <input type="email" id="regEmail" placeholder="E-post" required><br>
      <input type="text" id="regUserName" placeholder="Användare" required><br>
      <input type="password" id="regPassword" placeholder="Lösenord" required><br>
      <button type="submit">Registrera</button>
    </form>
    <div id="registerResult"></div>
  `;
  document.body.insertAdjacentHTML('afterbegin', formHtml);

  document.getElementById('registerForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    const Email = document.getElementById('regEmail').value;
    const UserName = document.getElementById('regUserName').value;
    const Password = document.getElementById('regPassword').value;

    const response = await fetch(`${baseApiUrl}/accounts/register`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ Email, UserName, Password }),
    });

    const resultDiv = document.getElementById('registerResult');
    if (response.ok) {
      resultDiv.innerHTML = '<span style="color:green">Användare skapad!</span>';
      console.log('User registered successfully');
    } else {
      resultDiv.innerHTML = '<span style="color:red">Misslyckades att skapa användare.</span>';
    }
  });
});
const ToDoList = document.querySelector('#ToDoList');

document.querySelector('#showToDoList').addEventListener('click', showToDoList);
document.querySelector('#login').addEventListener('click', login);
document.querySelector('#logout').addEventListener('click', logout);
document.querySelector('#register').addEventListener('click', register);

const baseApiUrl = 'https://localhost:5001/api';

async function listToDo() {
  console.log('List ToDoList');

  const response = await fetch(`${baseApiUrl}/todolists`, {
    method: 'GET',
    mode: 'cors',
    credentials: 'include',
  });

  if (response.ok) {
    const result = await response.json();
    console.log(result);
    displayProducts(result.data);
  } else {
    if (response.status === 401) displayError();
  }
}

async function login() {
  console.log('Log In');

  const response = await fetch(`${baseApiUrl}/accounts/login`, {
    method: 'POST',
    credentials: 'include',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ Email: 'Mohammed@outlook.com', Password: 'Password1!' }),
  });

  console.log(response);

  ToDoList.innerHTML = '';
}

async function register() {
  console.log('Register');

  const response = await fetch(`${baseApiUrl}/accounts/register`, {
    method: 'POST',
    credentials: 'include',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      UserName,
      Email,
      Password,
      RoleName
    }),
  });

  console.log(response);

  ToDoList.innerHTML = '';
}

async function logout() {
  console.log('Log out');

  const response = await fetch(`${baseApiUrl}/accounts/logout`, {
    method: 'POST',
    credentials: 'include',
  });

  console.log(response);
  ToDoList.innerHTML = '';
}

function displayProducts(todolists) {
  ToDoList.innerHTML = '';

  for (let todolist of todolists) {
    const div = document.createElement('div');
    div.textContent = todolist.name;

    ToDoList.appendChild(div);
  }
}

function displayError() {
  ToDoList.innerHTML = '<h2 style="color:red;">UNAUTHORIZED</h2>';
}
