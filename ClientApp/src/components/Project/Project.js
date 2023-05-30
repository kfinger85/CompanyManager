import React from 'react';
import { Collapse } from 'reactstrap';
import ProjectBody from './ProjectBody';
import { getProjects } from '../../services/dataService';

const Project = (props) => {
  const refreshProjectData = async () => {
    const updatedProjects = await getProjects();
    props.setProjects(updatedProjects);
    const updatedProject = updatedProjects.find(
      (p) => p.name === props.project.name
    );
    props.setActiveProject(updatedProject);
  };

  return (
    <div>
      <div>{props.project.name}</div>
      <Collapse isOpen={props.active}>
        {props.active === true ? (
          <ProjectBody {...props} updateProject={refreshProjectData} />
        ) : null}
      </Collapse>
    </div>
  );
};

export default Project;
