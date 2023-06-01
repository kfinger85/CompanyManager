export const Size = {
    SMALL: 'SMALL',
    MEDIUM: 'MEDIUM',
    BIG: 'BIG',
  };
  
  export const sizeToInt = (size) => {
    switch (size) {
      case Size.SMALL:
        return 1;
      case Size.MEDIUM:
        return 2;
      case Size.BIG:
        return 3;
      default:
        return 0; // Return 0 or throw an error if the size is not valid
    }
  };
  