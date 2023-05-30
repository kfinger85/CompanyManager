import axios from "axios";

const SERVER_ADDRESS = "https://localhost:7262/";


export function getQualification(name) {
  return axios
    .get(SERVER_ADDRESS + "qualifications/" + name)
    .then((res) => JSON.parse(res.request.response));
}

export function getQualifications() {
  return axios
    .get(SERVER_ADDRESS + "qualifications")
    .then((res) =>
      JSON.parse(res.request.response).sort((a, b) =>
        a.description.localeCompare(b.description)
      )
    );
}

export function createQualification(description) {
  return axios.post(SERVER_ADDRESS + "qualifications/" + description, {
    description: description,
  });
}

export function getWorker(name) {
  return axios
    .get(SERVER_ADDRESS + "workers/" + name)
    .then((res) => JSON.parse(res.request.response));
}

export function getWorkers() {
  return axios
    .get(SERVER_ADDRESS + "workers")
    .then((res) =>
      JSON.parse(res.request.response).sort((a, b) =>
        a.name.localeCompare(b.name)
      )
    );
}

export function createWorker(name, qualifications, salary) {
  return axios.post(SERVER_ADDRESS + "workers/" + name, {
    name: name,
    qualifications: qualifications,
    salary: salary,
  });
}

export function getProject(name) {
  return axios
    .get(SERVER_ADDRESS + "projects/" + name)
    .then((res) => JSON.parse(res.request.response));
}

export function getProjects() {
  return axios
    .get(SERVER_ADDRESS + "projects")
    .then((res) =>
      JSON.parse(res.request.response).sort((a, b) =>
        a.name.localeCompare(b.name)
      )
    );
}

export function createProject(name, qualifications, size) {
  return axios.post(SERVER_ADDRESS + "projects/" + name, {
    name: name,
    qualifications: qualifications,
    size: size,
  });
}

export function assignWorkerToProject(workerName, projectName) {
  return axios.put(SERVER_ADDRESS + "projects/assign", {
    worker: workerName,
    project: projectName,
  });
}

export function unassignWorkerFromProject(workerName, projectName) {
  return axios.put(SERVER_ADDRESS + "projects/unassign", {
    worker: workerName,
    project: projectName,
  });
}

  export function startProject(projectName) {
    return axios.put(SERVER_ADDRESS + 'projects/start', {
      name: projectName,
    });
  }
  export function finishProject(projectName) {
    return axios.put(SERVER_ADDRESS + 'projects/finish', {
      name: projectName,
    });
  }

  export function deleteProject(projectName) {
    return axios.put(SERVER_ADDRESS + 'projects/delete', {
      name: projectName,
    });
  }

  export function login(username, password) {
    return axios.post(SERVER_ADDRESS + "login", {
        username,
        password
    }).then(res => {
        if (res.status === 200) {
            return res.data; // If successful, return the user data
        } else {
            throw new Error(res.data); // If unsuccessful, throw an error with the server's response
        }
    });
}

