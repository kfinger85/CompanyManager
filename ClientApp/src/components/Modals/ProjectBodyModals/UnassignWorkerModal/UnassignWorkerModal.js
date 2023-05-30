import React, { useState, useEffect } from 'react';
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from 'reactstrap';
import { getWorkers, unassignWorkerFromProject, getProjects } from '../../../../services/dataService';
import { toast } from 'react-toastify';
import UnassignWorkerItem from './UnassignWorkerItem';

const UnassignWorkerModal = (props) => {
  const {updateProject, currentProjectWorkers, modal, project, onToggle, setProjects, setActiveProject, onSuccess } = props;
 
  const [workers, setWorkers] = useState([]);
  const [assignedWorkers, setAssignedWorkers] = useState(currentProjectWorkers);


  useEffect(() => {
    if (modal) {
      getWorkers().then(setWorkers);
      setAssignedWorkers(currentProjectWorkers);
    }
  }, [modal, project]);

  const toggleModal = () => {
    onToggle();
  };

  const unassignWorker = async (workerName) => {
    try {
      await unassignWorkerFromProject(workerName, project.name);
      toast.success(`Unassigned ${workerName} from ${project.name}`);
      updateProject(); 
    } catch (error) {
      toast.error("Failed to unassign worker. Please try again.");
    }
  };

  return (
    <Modal isOpen={modal} toggle={toggleModal}>
      <ModalHeader toggle={toggleModal}>Unassign Worker</ModalHeader>
      <ModalBody className="modal-body">
        <ul>
          {workers
            .sort((a, b) => a.name.localeCompare(b.name))
            .filter((worker) => assignedWorkers.includes(worker.name)) // Filter only assigned workers
            .map((worker, index) => {
              return (
                <UnassignWorkerItem
                  key={index}
                  worker={worker}
                  index={index}
                  unassignWorker={unassignWorker}
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
};

export default UnassignWorkerModal;
