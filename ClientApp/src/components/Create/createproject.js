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
  Dropdown,
  DropdownItem,
  DropdownMenu,
  DropdownToggle,
  InputGroup,
} from "reactstrap";
import { GoButton } from "../Buttons/projectbuttons";
import { CreateQualificationForWorkerModal } from "./createqualification";


export const CreateProject = (props) => {
  const [name, setName] = useState("");
  const [qualificationsChosen, setQualificationsChosen] = useState([]);
  const [size, setSize] = useState("");
  const [displaySize, setDisplaySize] = useState("");
  const [dropdownOpen, setDropdownOpen] = useState(false);


  const toggle = () => setDropdownOpen((prevState) => !prevState);


  const handleSelect = (value) => {
    setSize(value.toUpperCase());
    setDisplaySize(value);
  };


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
    <Modal isOpen={props.isOpen} toggle={props.close}>
      <ModalHeader className="project-modal-header" toggle={props.close}>
        Create A Project
      </ModalHeader>
      <ModalBody className="project-modal-body">
        <div className="project-input-all">
          <InputGroup className="project-buttongroup-container">
            <Collapse isOpen={props.description.length === 0}>
              <Input
                type="text"
                name="name"
                id="name"
                placeholder="Name"
                value={name}
                onChange={(e) => setName(e.target.value)}
                className="project-input-name"
              />
            </Collapse>
            <Collapse isOpen={props.description.length === 0}>
              <Dropdown isOpen={dropdownOpen} toggle={toggle}>
                <DropdownToggle className="drop-title" caret>
                  {displaySize || "Select a size"}
                </DropdownToggle>
                <DropdownMenu>
                  <DropdownItem onClick={() => handleSelect("Small")}>
                    Small
                  </DropdownItem>
                  <DropdownItem onClick={() => handleSelect("Medium")}>
                    Medium
                  </DropdownItem>
                  <DropdownItem onClick={() => handleSelect("Big")}>
                    Big
                  </DropdownItem>
                </DropdownMenu>
              </Dropdown>
            </Collapse>
          </InputGroup>
          <div className="project-qualification-box">
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
              size={size}
              setSize={setSize}
              qualificationsChosen={qualificationsChosen}
              setQualificationsChosen={setQualificationsChosen}
              projects={props.projects}
              setProjects={props.setProjects}
            />
          </ModalFooter>
        </div>
      </ModalBody>
    </Modal>
  );
};
