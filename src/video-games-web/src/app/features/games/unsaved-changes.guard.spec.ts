import { unsavedChangesGuard } from './unsaved-changes.guard';

describe('unsavedChangesGuard', () => {
  const run = (dirty: boolean) =>
    unsavedChangesGuard({ hasUnsavedChanges: () => dirty }, null!, null!, null!);

  it('allows navigation when the form is clean', () => {
    expect(run(false)).toBe(true);
  });
});
