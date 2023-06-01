import { useEffect, useState } from "react";
import ClickList from "../components/ClickList";
import {  getWorkers,  getQualifications,  getProjects} from "../services/dataService";
import LocationID from "../utils/location";
import {  pageStyle} from "../utils/styles";
import "../utils/css/worker.css";
import "../utils/css/projects.css";
import Worker from "../components/Worker/Worker";
import { CreateButton } from "../components/Buttons/workerbuttons";
import { CreateButtonForWorker } from "../components/Buttons/projectbuttons";
import { ToastContainer } from "react-toastify";

const Workers = (props) => {
  const [qualifications, setQualifications] = useState([]);
  const [workers, setWorkers] = useState([]);
  const [projects, setProjects] = useState([]);
  const [description, setDescription] = useState("");
  const [assignWorkerToProjectModalOpen, setAssignWorkerToProjectAssignModalOpen] = useState(false);
  const [unassignWorkerToProjectModalOpen, setUnassign] = useState(false);



   const active = LocationID("workers", workers, "name");

   const [projectsUpdated, setProjectsUpdated] = useState(false);



  useEffect(() => {
    getQualifications().then(setQualifications);
    getWorkers().then(setWorkers);
    getProjects().then(setProjects);
  }, [projectsUpdated]);

  return (
    <div style={pageStyle}>
      <ToastContainer
        className="toast-message"
        position="top-center"
        autoClose="2000"
        hideProgressBar="true"
      />
      <h1>This page displays a table containing all the workers.</h1>
      <CreateButton
        workers={workers}
        setWorkers={setWorkers}
        qualifications={qualifications}
        setQualifications={setQualifications}
        description={description}
        setDescription={setDescription}
        modalOpen={props.modalOpen}
        setModalOpen={props.setModalOpen}
      />
      <CreateButtonForWorker
        workers={workers}
        setWorkers={setWorkers}
        qualifications={qualifications}
        setQualifications={setQualifications}
        description={description}
        setDescription={setDescription}
        modalOpen={props.projectModalOpen}
        setModalOpen={props.setProjectModalOpen}
        projects={projects}
        setProjects={setProjects}
      />
      <ClickList
        active={active}
        list={workers}
        item={(worker, isActive) => (
          <Worker
            worker={worker}
            active={isActive}
            item={Worker}
            projectModalOpen={props.projectModalOpen}
            setProjectModalOpen={props.setProjectModalOpen}
            projects={projects}
            setProjects={setProjects}
            assignModalOpen={assignWorkerToProjectModalOpen}
            setAssignModalOpen={setAssignWorkerToProjectAssignModalOpen}
            unassignModalOpen={unassignWorkerToProjectModalOpen}
            setUnassignModalOpen={setUnassign}
            updateParentProjects={() =>  setProjectsUpdated(!projectsUpdated)} // Add this line
            path="/projects"
          />
        )}
        path="/workers"
        id="name"
      />
    </div>
  );
};

export default Workers;
