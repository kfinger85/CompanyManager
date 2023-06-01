import { CreateButton } from "./projectbuttons.js";

export const StartModalButton = (props) => {
  return (
    <div>
      <CreateButton
        projects={props.projects}
        setProjects={props.setProjects}
        qualifications={props.qualifications}
        setQualifications={props.setQualifications}
        description={props.description}
        setDescription={props.setDescription}
        projectModalOpen={props.projectModalOpen}
        setProjectModalOpen={props.setProjectModalOpen}
      />
    </div>
  );
};
