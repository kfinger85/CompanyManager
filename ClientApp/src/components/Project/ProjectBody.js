import React from 'react';
import {
  Card,
  CardBody,
  CardTitle,
  Row,
  Col,
  Button,
  ButtonGroup
} from 'reactstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserPlus, faUserMinus, faPlay } from '@fortawesome/free-solid-svg-icons';
import ClickList from '../ClickList';
import AssignWorkerModal from '../Modals/ProjectBodyModals/AssignWorkerModal/AssignWorkerModal';
import UnassignWorkerModal from '../Modals/ProjectBodyModals/UnassignWorkerModal/UnassignWorkerModal';
import { grayContainerStyle ,darkGrayContainerStyle } from '../../utils/styles';

import CustomCheckeredFlagButton from '../Buttons/CustomCheckeredFlagButton';

import { getWorkers, getProject, startProject, finishProject } from '../../services/dataService';

const ProjectBody = (props) => {
    const toggleAssignModal = (event) => {
      props.setModal(!props.modal);
      if (!props.modal) {
        getWorkers().then(props.setWorkers);
      }
      if (event) {
        event.stopPropagation();
      }
    };
  
    const onSuccess = async () => {
      const updatedProject = await getProject(props.project.name);
      props.setActiveProject(updatedProject);
      props.updateProject();
    };
  
    const toggleUnassignModal = (event) => {
      props.setUnassignModal(!props.unassignModal);
      if (event) {
        event.stopPropagation();
      }
    };
  
    const missingQualificationStyle = {
      color: "red",
      fontWeight: "bold",
    };
  
    const satisfiedQualificationStyle = {
      color: "green",
      fontWeight: "bold",
    };
  
        const handleStartProject = async (event) => {
          event.stopPropagation();
          await startProject(props.project.name);
          props.updateProject();
        };

        const handleFinishProject = async (event) => {
          event.stopPropagation();
          await finishProject(props.project.name);
          props.updateProject();
        }; 
  
        const canStart =
        (props.project.status === 'PLANNED' || props.project.status === 'SUSPENDED') &&
        props.project.missingQualifications.length === 0;
    return (
      <div style={{ ...grayContainerStyle, padding: "20px" }}>
        <Card style={{ ...grayContainerStyle, borderRadius: "8px" }}>
          <CardBody>
            <CardTitle tag="h3" style={{ marginBottom: "20px" }}>
              {props.project.name}
            </CardTitle>
            <Row style={{ marginBottom: "20px" }}>
              <Col>
                <div>
                  <strong>Status:</strong> {props.project.status}
                </div>
                <div>
                  <strong>Size:</strong> {props.project.size}
                </div>
              </Col>
              <Col>
                <div>
                  <strong>Qualifications:</strong>{" "}
                  <ClickList
                    list={props.project.qualifications}
                    styles={darkGrayContainerStyle}
                    path="/qualifications"
                    customStyles={(item) =>
                      props.project.missingQualifications.includes(item)
                        ? missingQualificationStyle
                        : satisfiedQualificationStyle
                    }
                  />
                </div>
              </Col>
              <Col>
                <div>
                  <strong>Workers:</strong>{" "}
                  <ClickList
                    list={props.project.workers}
                    styles={darkGrayContainerStyle}
                    path="/workers"
                  />
                </div>
              </Col>
            </Row>

<ButtonGroup>
            <Button outline
              disabled={
                props.project.status === "ACTIVE" ||
                props.project.status === "FINISHED"
              }
              onClick={toggleAssignModal}
              color={
                props.project.status === "ACTIVE" ||
                props.project.status === "FINISHED"
                  ? "secondary"
                  : "primary"
              }
            >
              <FontAwesomeIcon icon={faUserPlus} />
            </Button>
            <Button outline
              disabled={props.project.workers.length > 0 ? false : true}
              onClick={toggleUnassignModal}
              color={props.project.workers.length > 0 ? "danger" : "secondary"}
            >
              <FontAwesomeIcon icon={faUserMinus} />
        </Button>
        <Button outline
          disabled={!canStart}
          onClick={handleStartProject}
          color={canStart ? 'success' : 'secondary'}
        >
          <FontAwesomeIcon icon={faPlay} />
            </Button>
          
            <CustomCheckeredFlagButton 
            status={props.project.status}
            handleFinishProject={handleFinishProject}
            />
            </ButtonGroup>
          </CardBody>
        </Card>
        <AssignWorkerModal
          project={props.project}
          modal={props.modal}
          projectName={props.project.name}
          onToggle={toggleAssignModal}
          onAssignSuccess={onSuccess}
          updateProject={props.updateProject}
          setProjects={props.setProjects}
          setActiveProject={props.setActiveProject}
        />
  
        <UnassignWorkerModal
          project={props.project}
          modal={props.unassignModal}
          currentProjectWorkers={props.project.workers}
          onToggle={toggleUnassignModal}
          onSuccess={onSuccess}
          updateProject={props.updateProject}
        />
      </div>
    );
  };

export default ProjectBody;
