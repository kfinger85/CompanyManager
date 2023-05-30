import React, { useEffect } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import { assignWorkerToProject, getProjects } from '../../../services/dataService';
import { checkWorkerOverload } from '../ProjectBodyModals/AssignWorkerModal/Utils/workerUtils';
import './workerbodymodals.css';

const WorkerBodyAssignProjectModal = (props) => {


    useEffect(() => {
        getProjects().then(props.setProjects);
      }, [props.assignModalOpen, props.assignedWorker]);

    let eligibleProjects = [];

    props.projects.forEach((project) => {
      const missingQualifications = project.missingQualifications;
      const workerQualifications = new Set(props.worker.qualifications);
    
      if (missingQualifications.length > 0 && !project.workers.includes(props.worker)
      && project.status === "PLANNED"
      ) {
        for (const qualification of missingQualifications) {
          if (workerQualifications.has(qualification)) {
            eligibleProjects.push(project);
            break;
          }
        }
      }
    });
    

    const handleAssignWorkerToProject = (project) => {
        assignWorkerToProject(props.worker.name, project.name);
        getProjects().then(props.setProjects);
        props.updateParentProjects(); 


      };


      return (
        <>
          <Modal isOpen={props.assignModalOpen}
            toggle={() => props.setAssignModalOpen(!props.assignModalOpen)}
          >
            <ModalHeader>
              Assign {props.worker.name} to a Project
            </ModalHeader>
            <ModalBody>
              {eligibleProjects.length > 0 ? (
                  eligibleProjects.map((project) => {
                    const willOverload  = checkWorkerOverload(props.worker, project.size);
                    return (
                      !willOverload ? (
                        <div key={project.name}>
                          <h5
                            className='project-name'
                            onClick={() => handleAssignWorkerToProject(project)}
                          >
                            {project.name}
                          </h5>
                        </div>
                      ) : (
                        <div key={project.name + "_overload"}>Meets criteria of {project.name}, but will overload.</div>
                      ) 
                    );
                  })
              ) : (
                
                <div>No eligible projects found for this worker.</div>
              )}
            </ModalBody>
            <ModalFooter>
              <Button color="secondary" onClick={() => props.setAssignModalOpen(false)}>
                Cancel
              </Button>
            </ModalFooter>
          </Modal>
        </>
)
};
      
export default WorkerBodyAssignProjectModal;
