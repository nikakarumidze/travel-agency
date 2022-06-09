import { useState } from 'react';
import Button from '@mui/material/Button';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import { Link } from 'react-router-dom';

import classes from './Header.module.scss';
import { AppBar, Toolbar } from '@mui/material';

const Header = () => {
  const [anchorEl, setAnchorEl] = useState(null);
  const open = Boolean(anchorEl);
  const handleClick = (event) => setAnchorEl(event.currentTarget);
  const handleClose = () => setAnchorEl(null);

  return (
    <AppBar position='static' sx={{mb: 3, background: '#E5E5E5'}}>
      <Toolbar>
        <Button
          id='basic-button'
          aria-controls={open ? 'basic-menu' : undefined}
          aria-haspopup='true'
          aria-expanded={open ? 'true' : undefined}
          onClick={handleClick}
          endIcon={!open ? <KeyboardArrowDownIcon /> : <KeyboardArrowUpIcon />}
        >
          Cabinet
        </Button>

        <Menu
          id='basic-menu'
          anchorEl={anchorEl}
          open={open}
          onClose={handleClose}
          MenuListProps={{
            'aria-labelledby': 'basic-button',
          }}
        >
          <MenuItem>
            <Link onClick={handleClose} to='/'>
              Profile
            </Link>
          </MenuItem>
          <MenuItem>
            <Link onClick={handleClose} to='/'>
              My guests
            </Link>
          </MenuItem>
          <MenuItem>
            <Link onClick={handleClose} to='/'>
              My bookings
            </Link>
          </MenuItem>
        </Menu>
        <Button>Login</Button>
      </Toolbar>
    </AppBar>
  );
};

export default Header;