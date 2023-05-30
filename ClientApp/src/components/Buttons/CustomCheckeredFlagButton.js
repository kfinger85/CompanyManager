import React from "react";
import { Button } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faFlagCheckered } from "@fortawesome/free-solid-svg-icons";

const CustomCheckeredFlagButton = (props) => {
  return (
    <>
      <style>
        {`
          .custom-checkered-flag-button:hover {
            background-color: white !important;
          }
        `}
      </style>
      <Button outline
        className="custom-checkered-flag-button"
        disabled={props.status !== "ACTIVE"}
        onClick={props.handleFinishProject}
        color={props.status === "ACTIVE" ? "success" : "secondary"}
      >
        <FontAwesomeIcon icon={faFlagCheckered} style={{ color: "black" }} />
      </Button>
    </>
  );
};

export default CustomCheckeredFlagButton;
