import React, { useState } from 'react';
import { Tooltip } from 'reactstrap';

const WorkerItem = ({ worker, index, isAvailable, errors, assignWorker }) => {
  const workerItemClass = isAvailable ? 'worker-item' : 'worker-item unavailable';
  const tooltipTargetId = `worker-tooltip-${index}`;
  const [tooltipOpen, setTooltipOpen] = useState(false);

  const toggleTooltip = () => {
    setTooltipOpen(!tooltipOpen);
  };



  return (
    <div
      className={workerItemClass}
      onClick={() => isAvailable && assignWorker(worker.name)}
      id={tooltipTargetId}
      onMouseOver={toggleTooltip}
      onMouseOut={toggleTooltip}
    >
      {worker.name}
      <Tooltip
        isOpen={tooltipOpen}
        target={tooltipTargetId}
        toggle={toggleTooltip}
      >
        {isAvailable
          ? `Workload: ${worker.workload}, Qualifications: ${worker.qualifications.join(', ')}`
          : errors.map((error) => <div key={error}>{error}</div>)}
      </Tooltip>
    </div>
  );
};

export default WorkerItem;
