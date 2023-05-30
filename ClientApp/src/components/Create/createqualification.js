import React, { useState } from "react";
import ReactModal from "react-modal";
import { Input, InputGroup, FormGroup, Label } from "reactstrap";
import { GoButtonQualification } from "../Buttons/workerbuttons";

export const CreateQualification = (props) => {
  const [description, setDescription] = useState("");

  return (
    <ReactModal
      isOpen={props.isOpen}
      contentLabel="onRequestClose Example"
      onRequestClose={props.close}
      shouldCloseOnOverlayClick={true}
    >
      <div className="worker-input-all">
        <InputGroup>
          <Input
            className="worker-input-name"
            placeholder="Enter a new qualification"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />
        </InputGroup>
        <div className="worker-qualification-box">
          {props.qualifications.map(({ description }, index) => {
            return (
              <FormGroup check key={index}>
                <Label className="no-check-input" check>
                  <Input
                    className="no-check-box"
                    type="checkbox"
                    value={description}
                  />
                  {description}
                </Label>
              </FormGroup>
            );
          })}
        </div>
        <GoButtonQualification
          qualifications={props.qualifications}
          setQualifications={props.setQualifications}
          description={description}
          setDescription={setDescription}
        />
      </div>
    </ReactModal>
  );
};

export const CreateQualificationForWorkerModal = (props) => {
  return (
    <InputGroup className="qualification-input-in-worker-modal">
      <Input
        className="worker-modal-qualification-input"
        placeholder="Choose qualifications or create a new one here"
        value={props.description}
        onChange={(e) => props.setDescription(e.target.value)}
      />
      <GoButtonQualification
        qualifications={props.qualifications}
        setQualifications={props.setQualifications}
        description={props.description}
        setDescription={props.setDescription}
      />
    </InputGroup>
  );
};
