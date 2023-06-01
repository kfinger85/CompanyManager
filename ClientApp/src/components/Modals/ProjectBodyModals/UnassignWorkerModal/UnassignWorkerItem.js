import React, { useState } from 'react';
import { Tooltip } from 'reactstrap';

const UnassignWorkerItem = ({ worker, index, unassignWorker }) => {
  const workerItemClass = 'worker-item';
  const tooltipTargetId = `worker-tooltip-${index}`;
  const [tooltipOpen, setTooltipOpen] = useState(false);

  const toggleTooltip = () => {
    setTooltipOpen(!tooltipOpen);
  };

  return (
    <div
      className={workerItemClass}
      onClick={() => unassignWorker(worker.name)}
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
        {`Workload: ${worker.workload}, Qualifications: ${worker.qualifications.join(', ')}`}
      </Tooltip>
    </div>
  );
};

export default UnassignWorkerItem;
