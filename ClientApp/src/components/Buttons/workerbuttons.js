import { Button } from "reactstrap";
import {
  createWorker,
  getWorkers,
  createQualification,
  getQualifications,
} from "../../services/dataService";
import { CreateWorker } from "../Create/createworker.js";
import { toast } from "react-toastify";

export const GoButton = (props) => {
  const {
    name,
    setName,
    salary,
    setSalary,
    qualificationsChosen,
    setQualificationsChosen,
  } = props;

  const doesNameAlreadyExist = (name, workers) => {
    return workers.some((worker) => worker.name === name);
  };

  const handleCreateWorker = () => {
    const salaryNumber = parseInt(salary);
    setName("");
    setQualificationsChosen([]);
    setSalary("");
    createWorker(name, qualificationsChosen, salaryNumber).then(() =>
      getWorkers().then(props.setWorkers)
    );
    toast.success(`${name} has successfully been added to the company!`);
  };

  return (
    <Button
      disabled={
        !name ||
        name.trim().length === 0 ||
        !salary ||
        salary <= 0 ||
        qualificationsChosen.length === 0 ||
        doesNameAlreadyExist(name, props.workers)
      }
      onClick={handleCreateWorker}
      className={
        !name ||
        name.trim().length === 0 ||
        !salary ||
        salary <= 0 ||
        qualificationsChosen.length === 0 ||
        doesNameAlreadyExist(name, props.workers)
          ? "worker-button-invalid"
          : "worker-button-valid"
      }
    >
      Create Worker
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
  return (
    <div>
      <CreateWorker
        workers={props.workers}
        setWorkers={props.setWorkers}
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
      <Button
        outline={true}
        className="worker-button"
        onClick={() => props.setModalOpen(!props.modalOpen)}
      >
        Create A Worker
      </Button>
    </div>
  );
};

export const CreateButtonForQualification = (props) => {
  return (
    <div>
      <CreateWorker
        workers={props.workers}
        setWorkers={props.setWorkers}
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
