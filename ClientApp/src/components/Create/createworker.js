import React, { useState } from "react";
import {
  Input,
  FormGroup,
  Label,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Modal,
  Collapse,
} from "reactstrap";
import { GoButton } from "../Buttons/workerbuttons";
import { CreateQualificationForWorkerModal } from "./createqualification";

export const CreateWorker = (props) => {
  const [name, setName] = useState("");
  const [qualificationsChosen, setQualificationsChosen] = useState([]);
  const [salary, setSalary] = useState("");

  const handleQualificationChange = (e) => {
    if (e.target.checked) {
      setQualificationsChosen([...qualificationsChosen, e.target.value]);
    } else {
      setQualificationsChosen(
        qualificationsChosen.filter((q) => q !== e.target.value)
      );
    }
  };

  return (
    <Modal isOpen={props.isOpen} toggle={() => props.setModalOpen(!props.isOpen)}>
      <ModalHeader className="worker-modal-header" toggle={props.close}>
        Create A Worker
      </ModalHeader>
      <ModalBody className="worker-modal-body">
        <div className="worker-input-all">
          <div className="worker-input-fields">
          <Collapse isOpen={props.description.length === 0}>
            <Input
              type="text"
              name="name"
              id="name"
              placeholder="name"
              value={name}
              onChange={(e) => setName(e.target.value)}
              className="worker-input-name"
            />
          </Collapse>
          <Collapse isOpen={props.description.length === 0}>
            <Input
              className="worker-input-salary"
              type="number"
              name="salary"
              id="salary"
              placeholder="salary"
              value={salary}
              onChange={(e) => setSalary(e.target.value)}
            />
          </Collapse>
          </div>
          <div className="worker-qualification-box">
            <CreateQualificationForWorkerModal {...props} />
            {props.qualifications.map(({ description }, index) => {
              return (
                <FormGroup check key={index}>
                  <Label check>
                    <Input
                      type="checkbox"
                      value={description}
                      checked={qualificationsChosen.includes(description)}
                      onChange={handleQualificationChange}
                    />
                    {description}
                  </Label>
                </FormGroup>
              );
            })}
          </div>
          <ModalFooter>
            <GoButton
              name={name}
              setName={setName}
              salary={salary}
              setSalary={setSalary}
              qualificationsChosen={qualificationsChosen}
              setQualificationsChosen={setQualificationsChosen}
              workers={props.workers}
              setWorkers={props.setWorkers}
            />
          </ModalFooter>
        </div>
      </ModalBody>
    </Modal>
  );
};
