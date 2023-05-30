import React, { useState, useEffect } from 'react';
import { Modal, ModalHeader, ModalBody, ModalFooter, Button, Tooltip } from 'reactstrap';
import { getWorkers, assignWorkerToProject, getProjects } from '../../../../services/dataService';
import { toast } from 'react-toastify';
import WorkerItem from './WorkerItem';
import { checkWorkerAvailability } from './Utils/workerUtils';


const AssignWorkerModal = (props) => {
  const { modal, project, onToggle, setProjects, setActiveProject, onAssignSuccess} = props;
  const [workers, setWorkers] = useState([]);
  const [tooltipState, setTooltipState] = useState({ open: false, target: null });
  const [assignedWorker, setAssignedWorker] = useState(null);


  const updateProject = async () => {
    const updatedProjects = await getProjects();
    setProjects(updatedProjects);
    const updatedProject = updatedProjects.find((p) => p.name === project.name);
    setActiveProject(updatedProject);
    onAssignSuccess();
    const updatedWorkers = await getWorkers();
    setWorkers(updatedWorkers);

  };


  useEffect(() => {
    if (modal) {
      getWorkers().then(setWorkers);
    }
  }, [modal, project]);

  useEffect(() => {
    if (assignedWorker) {
      getWorkers().then(setWorkers);
      setAssignedWorker(null);
    }
  }, [assignedWorker]);

  const toggleModal = () => {
    onToggle();
  };

  const assignWorker = async (workerName) => {
    try {
      await assignWorkerToProject(workerName, project.name);
      toast.success(`Assigned ${workerName} to ${project.name}`);
      setAssignedWorker(workerName);
      onAssignSuccess();
      updateProject();
    } catch (error) {
      toast.error("Failed to assign worker. Please try again.");
    }
  };

  
  const onMouseOverWorker = (event) => {
    setTooltipState({ open: true, target: event.currentTarget });
  };

  const toggleTooltip = () => {
    setTooltipState((prevState) => ({ ...prevState, open: !prevState.open }));
  };
  
  

  return (
    <Modal isOpen={modal} toggle={toggleModal}>
      <ModalHeader toggle={toggleModal}>Assign Worker</ModalHeader>
      <ModalBody className="modal-body">
        <ul>
        {workers
  .sort((a, b) => a.name.localeCompare(b.name))
  .map((worker, index) => {
    const { isAvailable, errors } = checkWorkerAvailability(worker, project);

    return (
      <WorkerItem
        key={index}
        worker={worker}
        index={index}
        isAvailable={isAvailable}
        errors={errors}
        assignWorker={assignWorker}
        onMouseOverWorker={onMouseOverWorker}
        toggleTooltip={toggleTooltip}
        tooltipState={tooltipState}
      />
    );
  })}
        </ul>
      </ModalBody>
      <ModalFooter>
        <Button color="secondary" onClick={toggleModal}>
          Cancel
        </Button>
      </ModalFooter>
    </Modal>
  );
}
export default AssignWorkerModal;
