import { useState } from "react";
import { Button } from "reactstrap";
import {
  createProject,
  getProjects,
  createQualification,
  getQualifications,
} from "../../services/dataService";
import { CreateProject } from "../Create/createproject.js";
import { toast } from "react-toastify";
// import { CreateQualification } from "../Create/createqualification.js"

export const GoButton = (props) => {
  const {
    name,
    setName,
    size,
    setSize,
    qualificationsChosen,
    setQualificationsChosen,
  } = props;

  const doesNameAlreadyExist = (name, projects) => {
    return projects.some((project) => project.name === name);
  };

  const handleCreateProject = () => {
    setName("");
    setQualificationsChosen([]);
    setSize("");
    createProject(name, qualificationsChosen, size).then(() =>
      getProjects().then(props.setProjects)
    );
    toast.success(`Project ${name} has successfully been created!`);
  };

  return (
    <Button
      disabled={
        !name ||
        name.trim().length === 0 ||
        !size ||
        qualificationsChosen.length === 0 ||
        doesNameAlreadyExist(name, props.projects)
      }
      onClick={handleCreateProject}
      className={
        !name ||
        name.trim().length === 0 ||
        !size ||
        qualificationsChosen.length === 0 ||
        doesNameAlreadyExist(name, props.projects)
          ? "project-button-invalid"
          : "project-button-valid"
      }
    >
      Create Project
    </Button>
  );
};

export const GoButtonQualification = (props) => {
  const { description, setDescription, qualifications, setQualifications } =
    props;

  const doesQualificationAlreadyExist = (description, qualifications) => {
    return qualifications.some(
      (qualification) => qualification.description === description
    );
  };

  const handleCreateQualification = () => {
    setDescription("");
    createQualification(description).then(() =>
      getQualifications().then(setQualifications)
    );
  };

  return (
    <>
      <Button
        disabled={
          !description ||
          doesQualificationAlreadyExist(description, qualifications)
        }
        onClick={handleCreateQualification}
        className={
          !description
            ? "qualification-button-invalid"
            : "qualification-button-valid"
        }
      >
        Create Qualification
      </Button>
    </>
  );
};

export const CreateButton = (props) => {
  // const [modalOpen, setModalOpen] = useState(false);
  // const [modalOpenQualification, setModalOpenQualification] = useState(false);
  return (
    <div>
      <CreateProject
        projects={props.projects}
        setProjects={props.setProjects}
        qualifications={props.qualifications}
        setQualifications={props.setQualifications}
        isOpen={props.projectModalOpen}
        setProjectModalOpen={props.setProjectModalOpen}
        description={props.description}
        setDescription={props.setDescription}
        close={() => {
          props.setProjectModalOpen(false);
        }}
      />
      <Button
        // style={{ color: "blue" }}
        outline={true}
        onClick={() => props.setProjectModalOpen(!props.projectModalOpen)}
      >
        Create A Project
      </Button>
    </div>
  );
};

export const CreateButtonForWorker = (props) => {
  return (
    <div>
      <CreateProject
        projects={props.projects}
        setProjects={props.setProjects}
        qualifications={props.qualifications}
        setQualifications={props.setQualifications}
        isOpen={props.modalOpen}
        setModalOpen={props.setModalOpen}
        description={props.description}
        setDescription={props.setDescription}
        close={() => {
          props.setModalOpen(false);
        }}
      />
    </div>
  );
};
