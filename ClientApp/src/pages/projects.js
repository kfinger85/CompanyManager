import { useEffect, useState } from "react";
import ClickList from "../components/ClickList";
import { getProjects, getQualifications } from "../services/dataService";
import LocationID from "../utils/location";
import { pageStyle } from "../utils/styles";

import { ToastContainer } from "react-toastify";
import "../utils/css/assignWorkerModal.css";
import { StartModalButton } from "../components/Buttons/startbutton";
import "../utils/css/start.css";

import Project from "../components/Project/Project";

const Projects = (props) => {
  const [projects, setProjects] = useState([]);
  const [qualifications, setQualifications] = useState([]);
  const [modal, setModal] = useState(false);
  const [workers, setWorkers] = useState([]);
  const [activeProject, setActiveProject] = useState(null);
  const [unassignModal, setUnassignModal] = useState(false);
  const [name, setName] = useState("");
  const [availableProjects, setAvailableProjects] = useState([]);
  const [description, setDescription] = useState("");

  const active = LocationID("projects", projects, "name");
  useEffect(() => {
    getQualifications().then(setQualifications);
    const fetchProjectsAndUpdateActiveProject = async () => {
      const fetchedProjects = await getProjects();
      setProjects(fetchedProjects);

      if (active !== -1) {
        setActiveProject(
          fetchedProjects.find((p) => p.name === projects[active].name)
        );
      }
    };

    fetchProjectsAndUpdateActiveProject();
  }, [active]);

  useEffect(() => {
    getProjects().then((projects) => {
      const filteredProjects = projects.filter(
        (project) =>
          (project.status === "PLANNED" || project.status === "SUSPENDED") &&
          project.missingQualifications.length === 0
      );
      setAvailableProjects(filteredProjects);
    });
  }, [projects]);

  const updateProject = async () => {
    const updatedProjects = await getProjects();
    setProjects(updatedProjects);
    if (active !== -1) {
      const updatedActiveProject = updatedProjects.find(
        (p) => p.name === projects[active].name
      );
      setActiveProject(updatedActiveProject);
    }
  };

  return (
    <div style={pageStyle}>
      <ToastContainer
        position="top-center"
        autoClose="2000"
        hideProgressBar="true"
      />
      <h1>This page displays a table containing all the projects.</h1>
      <StartModalButton
        projects={projects}
        setProjects={setProjects}
        qualifications={qualifications}
        setQualifications={setQualifications}
        description={description}
        setDescription={setDescription}
        activeProject={activeProject}
        setActiveProject={setActiveProject}
        name={name}
        setName={setName}
        availableProjects={availableProjects}
        setAvailableProjects={setAvailableProjects}
        projectModalOpen={props.projectModalOpen}
        setProjectModalOpen={props.setProjectModalOpen}
      />
      <ClickList
        active={active}
        list={projects}
        item={(project, isActive) => (
          <Project
            project={project}
            setProjects={setProjects}
            active={isActive}
            modal={modal}
            setModal={setModal}
            workers={workers}
            setWorkers={setWorkers}
            activeProject={activeProject}
            setActiveProject={setActiveProject}
            updateProject={updateProject}
            unassignModal={unassignModal}
            setUnassignModal={setUnassignModal}
          />
        )}
        path="/projects"
        id="name"
      />
    </div>
  );
};

export default Projects;
