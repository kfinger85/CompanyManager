import { sizeToInt } from './sizeUtils';

export function checkWorkerOverload(worker, projectSize) {
  const projectSizeInt = sizeToInt(projectSize);
  return worker.workload + projectSizeInt > 12;
}

export function checkWorkerAvailability(worker, project) {
  const errors = [];

  // Check if the project is not in the ACTIVE or FINISHED state
  if (project.status === 'ACTIVE' || project.status === 'FINISHED') {
    errors.push('Project is in the ACTIVE or FINISHED state');
  }

  // Check if the worker is already assigned to the project
  if (worker.projects.includes(project.name)) {
    errors.push(`${worker.name} is already assigned to ${project.name}`);
  }

  // Convert the project size to an integer using the sizeToInt function

  // Check if the worker will become overloaded
  if (checkWorkerOverload(worker, project.size)) {
    errors.push(`${worker.name} will become overloaded`);
  }

  // Check if the worker has at least one missing qualification
  const hasNeededQualification = project.missingQualifications.some((qual) =>
    worker.qualifications.includes(qual)
  );
  if (!hasNeededQualification) {
    errors.push(`${worker.name} does not have any missing qualifications`);
  }

  const isAvailable = errors.length === 0;
  return { isAvailable, errors };
}