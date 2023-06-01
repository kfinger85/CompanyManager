import { useState } from "react";
import { Collapse } from "reactstrap";
import WorkerBody from "./WorkerBody";

const Worker = (props) => {
  const [isOpen, setIsOpen] = useState(false);


  return (
    <div onClick={() => setIsOpen(!isOpen)}>
      <div>{props.worker.name}</div>
      <Collapse isOpen={props.active}>
      {props.active === true ? (
        <WorkerBody
          {...props}
          close={() => {
            props.setProjectModalOpen(false);
          }}
          projectModalOpen={props.projectModalOpen}
          updateParentProjects={props.updateParentProjects}
          />
        ) : null}
        </Collapse>
      </div>
    );
  };
  
  export default Worker;
