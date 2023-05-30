import { useState } from "react";
import { Collapse, Card, CardBody, CardTitle, Row, Col, Button, ButtonGroup, Tooltip } from "reactstrap";
import { faUserPlus, faUserMinus } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import ClickList from "../ClickList/index";
import WorkerBodyAssignProjectModal from '../Modals/WorkerBodyModals/WorkerBodyAssignProjectModal';
import WorkerBodyUnassignProjectModal from "../Modals/WorkerBodyModals/WorkerBodyUnassignProjectModal";
import {
  grayContainerStyleWorkerQual,
  grayContainerStyleWorkerProject,
  grayContainerStyle,
} from "../../utils/styles";

const WorkerBody = (props) => {
  const [tooltipAddOpen, setTooltipAddOpen] = useState(false);
  const [tooltipRemoveOpen, setTooltipRemoveOpen] = useState(false);

  const toggleTooltipAdd = () => setTooltipAddOpen(!tooltipAddOpen);
  const toggleTooltipRemove = () => setTooltipRemoveOpen(!tooltipRemoveOpen);



  const handleClick = (event) => {
    event.stopPropagation();
  };

  const workerQualifications = new Set(props.worker.qualifications);

  const eligibleProjects = props.projects.filter((project) => {
    const missingQualifications = project.missingQualifications;
  
    if (
      missingQualifications.length > 0 &&
      !project.workers.includes(props.worker) &&
      project.status === "PLANNED"
    ) {
      return missingQualifications.some((qualification) =>
        workerQualifications.has(qualification)
      );
    }
  
    return false;
  });
  return (
    <div style={{ ...grayContainerStyle, padding: "20px" }}>
      <Card style={{ ...grayContainerStyle, borderRadius: "8px" }}>
        <CardBody onClick={handleClick}>
          <CardTitle tag="h3" style={{ marginBottom: "20px" }}>
            {props.worker.name}
          </CardTitle>

          <Row style={{ marginBottom: "20px" }}>
            <Col>
              <div>
                <strong>Salary:</strong> ${props.worker.salary.toLocaleString()}
              </div>
              <div>
                <strong>Workload:</strong> {props.worker.workload}
              </div>                
              <div> 
              </div>
            </Col>
            <Col>
              <div className="gray-container-worker-qual">
                <strong>Qualifications:</strong>{" "}
                <ClickList
                  list={props.worker.qualifications}
                  styles={grayContainerStyleWorkerQual}
                  path="/qualifications"
                  className="worker-qualifications"
                />
              </div>
            </Col>
            <Col>
              <div className="gray-container-worker-project">
                <strong
                  className={props.worker.projects.length === 0 ? "red" : ""}
                >
                  Projects:
                </strong>{" "}
                <ClickList
                  list={props.worker.projects}
                  styles={grayContainerStyleWorkerProject}
                  path="/projects"
                />
              </div>
              <div>
                <Collapse isOpen={props.worker.projects.length === 0}>
                  <div>{props.worker.name} currently has no projects!</div>
                </Collapse>
              </div>
            </Col>
          </Row>
          <ButtonGroup>
  <div id="addTooltip">
    <Button
      outline={true}
      color={eligibleProjects.length !== 0 ? "primary" : "secondary" }
      disabled={eligibleProjects.length === 0}
      onClick={() => props.setAssignModalOpen(true)}
      >
      <FontAwesomeIcon icon={faUserPlus} />
    </Button>
  </div>
  <div id="removeTooltip">
    <Button
      outline={true}
      color={props.worker.projects.length!== 0 ? "danger" : "secondary" }

      disabled={props.worker.projects.length === 0}
      onClick={() => props.setUnassignModalOpen(true)}
    >
      <FontAwesomeIcon icon={faUserMinus} />
    </Button>
  </div>
</ButtonGroup>
<Tooltip
  placement="top"
  isOpen={tooltipAddOpen}
  target="addTooltip"
  toggle={toggleTooltipAdd}
>
    {eligibleProjects.length !== 0 ? 'Add To Project' : 'No Eligible Projects'}
</Tooltip>
<Tooltip
  placement="top"
  isOpen={tooltipRemoveOpen}
  target="removeTooltip"
  toggle={toggleTooltipRemove}
>
{props.worker.projects.length!== 0 ? 'Remove From Project' : 'No Projects Assigned'}
  
</Tooltip>
  
          <WorkerBodyAssignProjectModal 
                  worker={props.worker}
                  projects={props.projects}
                  setProjects={props.setProjects}
                  assignModalOpen={props.assignModalOpen}
                  setAssignModalOpen={props.setAssignModalOpen}
                  updateParentProjects={props.updateParentProjects}
     
                />
                <WorkerBodyUnassignProjectModal
                    key={props.unassignModalOpen.toString()}
                    worker={props.worker}
                    projects={props.projects}
                    setProjects={props.setProjects}
                    unassignModalOpen={props.unassignModalOpen}
                    setUnassignModalOpen={props.setUnassignModalOpen}
                    unassignWorker={props.unassignWorker}
                    updateParentProjects={props.updateParentProjects}
                    
                />
        </CardBody>
      </Card>

    </div>
  );
};

export default WorkerBody;
