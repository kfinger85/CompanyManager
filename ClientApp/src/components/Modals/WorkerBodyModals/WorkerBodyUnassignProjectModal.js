import React, { useEffect, useState } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import { unassignWorkerFromProject, getProjects } from '../../../services/dataService';
import './workerbodymodals.css';


const WorkerBodyUnassignProjectModal = (props) => {
    const [assignedProjects, setAssignedProjects] = useState(props.worker.projects);

    useEffect(() => {
        getProjects().then(props.setProjects);
    }, []);

    const handleUnassignWorkerFromProject = async (project) => {
        await unassignWorkerFromProject(props.worker.name, project);
        getProjects().then(props.setProjects);
        setAssignedProjects(assignedProjects.filter(p => p !== project));
        props.updateParentProjects();
      };
     


    return (
        <>
            <Modal isOpen={props.unassignModalOpen}
                toggle={() => props.setUnassignModalOpen(!props.unassignModalOpen)}>
                <ModalHeader>
                        Unassign {props.worker.name} from a Project
                </ModalHeader>
                <ModalBody>
                    {assignedProjects.length > 0 ? (
                        assignedProjects.map((project) => (
                            <div key={project}>
                                <h5
                                    className={`project-name`}
                                    onClick={() => handleUnassignWorkerFromProject(project)}>
                                        {project}
                                </h5>
                            </div>
                        ))
                    ) : (
                        <div>This worker is not assigned to any projects.</div>
                    )}
                </ModalBody>
                <ModalFooter>
                    <Button color="secondary" onClick={() => props.setUnassignModalOpen(false)}>
                        Cancel
                    </Button>
                </ModalFooter>
            </Modal>
        </>
    );
};

export default WorkerBodyUnassignProjectModal;
