import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import ClickList from "../components/ClickList";
import {
  getQualifications,
  createQualification,
} from "../services/dataService";
import LocationID from "../utils/location";
import {
  Button,
  Collapse,
  Input,
  InputGroup,
  Card,
  CardBody,
  CardTitle,
  Row,
  Col,
} from "reactstrap";
import {
  grayContainerStyle,
  darkGrayContainerStyle,
  grayContainerStyleWorkerQual,
  pageStyle,
  qualificationsNoWorkersMessage,
  grayContainerStyleWorkerProject,
} from "../utils/styles";
import "../utils/css/qualifications.css";
import {
  CreateButton,
  CreateButtonForQualification,
} from "../components/Buttons/workerbuttons";
import { ToastContainer } from "react-toastify";

const Qualification = (props) => {
  return (
    <div>
      <div>{props.qualification.description}</div>
      {props.active === true ? (
        <QualificationBody
          {...props}
          close={() => {
            props.setModalOpen(false);
          }}
          modalOpen={props.modalOpen}
        />
      ) : null}
    </div>
  );
};

const SendToWorkerButton = (props) => {
  const sendToCreateWorker = () => {
    props.setModalOpen(true);
  };

  return (
    <div>
      <Button
        outline={true}
        className="go-to-worker-button"
        onClick={sendToCreateWorker}
      >
        Create Worker With This Qualification
      </Button>
    </div>
  );
};

const QualificationBody = (props) => {
  const [button, setButton] = useState([SendToWorkerButton(props)]);

  return (
    <div style={{ ...grayContainerStyle, padding: "20px" }}>
      <Card style={{ ...grayContainerStyle, borderRadius: "8px" }}>
        <CardBody>
          <CardTitle tag="h3" style={{ marginBottom: "20px" }}>
            {props.qualification.description}
          </CardTitle>
          <Row style={{ marginBottom: "20px" }}>
            <Col>
              <div className="gray-container-worker-qual">
                <strong
                  className={
                    props.qualification.workers.length === 0 ? "red" : ""
                  }
                >
                  Workers:
                </strong>{" "}
                <ClickList
                  list={props.qualification.workers}
                  styles={grayContainerStyleWorkerQual}
                  path="/workers"
                  className="worker-qualifications"
                />
              </div>
              <div>
                <Collapse isOpen={props.qualification.workers.length === 0}>
                  <div>No workers have this qualification</div>
                </Collapse>
              </div>
            </Col>
            <Col>
              <br></br>
              <div style={grayContainerStyle}>
                <Collapse isOpen={props.qualification.workers.length === 0}>
                  <SendToWorkerButton {...props} />
                </Collapse>
              </div>
            </Col>
          </Row>
        </CardBody>
      </Card>
    </div>
  );
};

const CreateQualification = (props) => {
  const [description, setDescription] = useState("");

  const handleCreateQualification = () => {
    setDescription("");
    createQualification(description).then(() =>
      getQualifications().then(props.setQualifications)
    );
  };

  const doesQualificationAlreadyExist = (description, qualifications) => {
    return qualifications.some(
      (qualification) => qualification.description === description
    );
  };

  return (
    <div>
      <InputGroup>
        <Input
          placeholder="Create a new qualification"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
        <Button
          disabled={
            !description ||
            description.trim().length === 0 ||
            doesQualificationAlreadyExist(description, props.qualifications)
          }
          className="qualification-button"
          onClick={handleCreateQualification}
        >
          Add
        </Button>
      </InputGroup>
      <br></br>
    </div>
  );
};

const Qualifications = (props) => {
  const [qualifications, setQualifications] = useState([]);
  const [workers, setWorkers] = useState([]);
  const [description, setDescription] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    getQualifications().then(setQualifications);
  }, [workers]);

  const active = LocationID("qualifications", qualifications, "description");

  return (
    <div style={pageStyle}>
      <ToastContainer
        className="toast-message"
        position="top-center"
        autoClose="2000"
        hideProgressBar="true"
      />
      <h1>This page displays a table containing all the qualifications.</h1>
      <CreateButtonForQualification
        workers={workers}
        setWorkers={setWorkers}
        qualifications={qualifications}
        setQualifications={setQualifications}
        description={description}
        setDescription={setDescription}
        modalOpen={props.modalOpen}
        setModalOpen={props.setModalOpen}
      />
      <CreateQualification
        qualifications={qualifications}
        setQualifications={setQualifications}
        setModalOpen={props.setModalOpen}
      />
      <ClickList
        active={active}
        list={qualifications}
        item={(qualification, isActive) => (
          <Qualification
            qualification={qualification}
            active={isActive}
            list={qualifications}
            item={Qualification}
            modalOpen={props.modalOpen}
            setModalOpen={props.setModalOpen}
            navigate={navigate}
            path="/workers"
          />
        )}
        path="/qualifications"
        id="description"
      />
    </div>
  );
};

export default Qualifications;
